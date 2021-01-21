using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConferenceApiTDD.Controllers;
using ConferenceApiTDD.Lib;
using ConferenceApiTDD.Models;
using Xunit;
using ConferenceApiTDD.Test.Init;

namespace ConferenceApiTDD.Test
{
    public class SpekaerController_Tests
    {

        [Fact]
        public async Task WhenSpeakerExists_ReturnsOkWithSpeakerInformation()
        {
            const string SPEAKER_NAME = "Scott Guthrie";

            var service = Initialiser.ConferenceServiceFactory(
                SpeakerControllerResponses.FoundSpeakerJsonResponse(SPEAKER_NAME),
                SpeakerControllerResponses.SpeakerResponse(SPEAKER_NAME));
            var sut = new SpeakerController(new NullLogger<SpeakerController>(), service);

            var result = await sut.ReadSessionsWithTopics(SPEAKER_NAME, new DateTime()) as OkObjectResult;

            Assert.IsType<Speaker>(result.Value);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(SPEAKER_NAME, ((Speaker)result.Value).Name);
        }
        [Fact]
        public async Task WhenSpeakerDoesNotExists_ReturnsBadRequest()
        {
            const string SPEAKER_NAME = "Scott Guthrie";

            var service = Initialiser.ConferenceServiceFactory(
                SpeakerControllerResponses.NullSpeakerJsonResponse,
                SpeakerControllerResponses.SpeakerResponse(SPEAKER_NAME));
            var sut = new SpeakerController(new NullLogger<SpeakerController>(), service);

            var result = await sut.ReadSessionsWithTopics(SPEAKER_NAME, new DateTime()) as ObjectResult;

            Assert.Contains("not found", result.Value.ToString());
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public async Task WhenSpeakerNameNotEntered_ReturnsBadRequest()
        {
            string SPEAKER_NAME = string.Empty;

            var service = Initialiser.ConferenceServiceFactory(
                SpeakerControllerResponses.FoundSpeakerJsonResponse(SPEAKER_NAME),
                SpeakerControllerResponses.SpeakerResponse(SPEAKER_NAME));
            var sut = new SpeakerController(new NullLogger<SpeakerController>(), service);

            var result = await sut.ReadSessionsWithTopics(SPEAKER_NAME, new DateTime()) as ObjectResult;

            Assert.Contains("is missing", result.Value.ToString());
            Assert.Equal(400, result.StatusCode);
        }
    }
}
