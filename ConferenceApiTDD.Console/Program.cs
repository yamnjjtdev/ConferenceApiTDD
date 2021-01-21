using ConferenceApiTDD.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConferenceApiTDD.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var _client = new HttpClient
            {
                BaseAddress = new Uri("https://apicandidates.azure-api.net/conference/")
            };
            _client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            _client.DefaultRequestHeaders.Add("Api-version", "v1");
            _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "67f50e1f75e84a78856eb15d8ec10a48");
            var response = await _client.GetAsync("speakers");

            if (response.IsSuccessStatusCode)
            {
                var jsonOpts = new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true };
                var strResponse = await response.Content.ReadAsStringAsync();
                var contentStream = await response.Content.ReadAsStreamAsync();

                // json filter found to be bit messy for the api data strucutre
                JToken source = JToken.Parse(strResponse);
                List<JToken> token = source.SelectToken("$..*[?(@.value=='Scott Guthrie')]").ToList();

                var jsonResponse = await JsonSerializer.DeserializeAsync<JsonCollection>(contentStream, jsonOpts);

                var speaker = jsonResponse.collection.items.First(m => m.data.Find(e => e.value == "Scott Guthrie") != null);

                var sessionsLnk = speaker.links.First(m => m.rel.EndsWith("sessions")).href;

                var sessionsResponse = await _client.GetAsync(sessionsLnk);
                if (sessionsResponse.IsSuccessStatusCode)
                {
                    var sessionsStream = await sessionsResponse.Content.ReadAsStreamAsync();
                    var sessionsJsonResponse = await JsonSerializer.DeserializeAsync<JsonCollection>(sessionsStream, jsonOpts);


                }

            }

        }
    }
}
