using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ConferenceApiTDD.Config;
using ConferenceApiTDD.Models;

namespace ConferenceApiTDD.Lib
{
    public class ConferenceApiService : IConferenceApiService
    {
        private readonly ConferenceApi _confApiConfig;
        private readonly IHttpClientFactory _httpFactory;

        private const string RESOURCE_SPEAKERS = "speakers";
        private const string REL_SPEAKER_SESSIONS = "http://tavis.net/rels/sessions";
        private const string REL_SESSION_TOPICS = "http://tavis.net/rels/topics";
        private const string DATETIME_FIELD_NAME = "Timeslot";
        private const string TITLE_FIELD_NAME = "Title";

        private readonly HttpClient _client;

        public ConferenceApiService(IOptions<ConferenceApi> opts, IHttpClientFactory httpFactory)
        {
            _confApiConfig = opts.Value;
            _httpFactory = httpFactory;

            _client = _httpFactory.CreateClient("ConferenceApiClient");
            _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _confApiConfig.AuthKey);
            _client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            _client.DefaultRequestHeaders.Add("Api-version", "v1");
        }


        private async Task<JsonCollection> ConvertToCollection(HttpResponseMessage msg)
        {
            var jsonOpts = new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true };
            var contentStream = await msg.Content.ReadAsStreamAsync();
            var retCollection = await JsonSerializer.DeserializeAsync<JsonCollection>(contentStream, jsonOpts);

            return retCollection;

        }

        public async Task<Item> FindSpeaker(string speakerName)
        {
            var speakersResponse = await _client.GetAsync($"{_confApiConfig.ApiBaseUrl}{RESOURCE_SPEAKERS}/");

            if (speakersResponse.IsSuccessStatusCode)
            {
                var speakersJson = await ConvertToCollection(speakersResponse);
                // case insensitive
                var speakerFound = speakersJson.collection.items.FirstOrDefault(m =>
                m.data.Find(e => e.value.ToLower() == speakerName.ToLower()) != null);
                return speakerFound;
            }
            else
            {
                throw new ConferenceApiException(speakersResponse.StatusCode,
                    "Error response from Conference Api while reading speakers: " + speakersResponse.ReasonPhrase);
            }

        }

        public async Task<Speaker> ReadSessionsWithTopics(Item speaker, DateTime? datetime = null)
        {
            var retSpeaker = new Speaker() { Name = speaker.data[0].value, Sessions = new List<Session>() };
            var foundLink = speaker.links.Find(e => e.rel.Equals(REL_SPEAKER_SESSIONS));
            var linkToSessions = foundLink.href;
            var sessionsResponse = await _client.GetAsync(new Uri(linkToSessions));


            if (sessionsResponse.IsSuccessStatusCode)
            {
                var sessionsJson = await ConvertToCollection(sessionsResponse);
                var sessionsItems = new List<Item>();
                if (datetime != null)
                {
                    sessionsItems = sessionsJson.collection.items.FindAll(m =>
                    m.data.Find(e =>
                    e.name == DATETIME_FIELD_NAME && DateComparer.IsDateTimeWithin((DateTime)datetime, e.value)) != null);
                }
                else
                {
                    sessionsItems = sessionsJson.collection.items;
                }
                foreach (Item session in sessionsItems)
                {
                    var topics = await ReadTopics(session);
                    retSpeaker.Sessions.Add(
                        new Session()
                        {
                            Title = session.data.Find(m => m.name == TITLE_FIELD_NAME).value,
                            Timeslot = session.data.Find(m => m.name == DATETIME_FIELD_NAME).value,
                            Topics = topics
                        });
                }
            }
            else
            {
                throw new ConferenceApiException(sessionsResponse.StatusCode,
                    "Error response from Conference Api while reading sessions: " + sessionsResponse.ReasonPhrase);
            }

            return retSpeaker;
        }

        private async Task<List<Topic>> ReadTopics(Item session)
        {
            var foundLink = session.links.Find(e => e.rel.Equals(REL_SESSION_TOPICS));
            var linkToSessions = foundLink.href;
            var topicsResponse = await _client.GetAsync(new Uri(linkToSessions));
            var retTopics = new List<Topic>();


            if (topicsResponse.IsSuccessStatusCode)
            {
                var topicsJson = await ConvertToCollection(topicsResponse);

                topicsJson.collection.items.ForEach(e =>
                {
                    retTopics.Add(new Topic()
                    {
                        Title = e.data.Find(m => m.name == TITLE_FIELD_NAME).value
                    });
                });
            }
            else
            {
                throw new ConferenceApiException(topicsResponse.StatusCode,
                    "Error response from Conference Api while reading topics: " + topicsResponse.ReasonPhrase);
            }

            return retTopics;
        }

    }
}
