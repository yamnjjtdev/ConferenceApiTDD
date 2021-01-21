using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConferenceApiTDD.Lib;
using ConferenceApiTDD.Models;
using Xunit;
using ConferenceApiTDD.Test.Init;

namespace ConferenceApiTDD.Test
{
    public class ConferenceService_Tests
    {
        public static IEnumerable<object[]> ExceptionResponses => BuildExceptionResponses();
        private static IEnumerable<object[]> BuildExceptionResponses()
        {
            return new List<object[]>
            {
            new object[] {
                401,
                new StringContent(JsonSerializer.Serialize(new { Code = 401, Message = "Invalid Auth key." })),
                HttpStatusCode.Unauthorized },
            new object[] {
                404,
                new StringContent(JsonSerializer.Serialize(new { Code = 404, Message = "Speaker not found." })),
                HttpStatusCode.NotFound },
            new object[] {
                500,
                new StringContent(JsonSerializer.Serialize(new { Code = 500, Message = "Internal Error." })),
                HttpStatusCode.InternalServerError }
            };
        }

        [Theory]
        [MemberData(nameof(ExceptionResponses))]
        public async Task WhenErrorFindingSpeaker_ReturnsConferenceApiException(int errorCode, StringContent response, HttpStatusCode statusCode)
        {
            ConferenceApiService sut = Initialiser.BuildService(response, statusCode);

            var result = await Assert.ThrowsAsync<ConferenceApiException>(() => sut.FindSpeaker("Random Speaker"));
            Assert.Equal(errorCode, (int)result.StatusCode);
        }

        [Fact]
        public async Task WhenSpeakerFound_ReturnsSpeaker()
        {
            const string NAME_TO_SEARCH = "Scott Guthrie";
            var opts = Initialiser.ConferenceApiConfig();
            var clientFactoryForSpeakers = Initialiser.ConferenceClientFactory(ConferenceApiResponses.SpeakersResponse);
            var sut = new ConferenceApiService(opts, clientFactoryForSpeakers);

            var result = await sut.FindSpeaker(NAME_TO_SEARCH);
            //, new DateTime(2013,12,04,11,10,00)

            Assert.IsType<Item>(result);
            Assert.Equal(NAME_TO_SEARCH, result.data[0].value);
        }

        [Fact]
        public async Task WhenSpeakerNotFound_ReturnsNull()
        {
            const string NAME_TO_SEARCH = "Scott Guthriex";
            var opts = Initialiser.ConferenceApiConfig();
            var clientFactoryForSpeakers = Initialiser.ConferenceClientFactory(ConferenceApiResponses.SpeakersResponse);
            var sut = new ConferenceApiService(opts, clientFactoryForSpeakers);

            var result = await sut.FindSpeaker(NAME_TO_SEARCH);
            //, new DateTime(2013,12,04,11,10,00)

            Assert.Null(result);
        }

    }
}
