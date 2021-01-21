using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ConferenceApiTDD.Config;
using ConferenceApiTDD.Lib;
using ConferenceApiTDD.Models;

namespace ConferenceApiTDD.Test.Init
{
    public static class Initialiser
    {
        public static IHttpClientFactory ConferenceClientFactory(StringContent content, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = content
                });
            var client = new HttpClient(handler.Object);
            var clientFactory = new Mock<IHttpClientFactory>();
            clientFactory.Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(client);
            return clientFactory.Object;
        }

        public static IConferenceApiService ConferenceServiceFactory(Item speakerSearchResult, Speaker speakerResult)
        {
            var service = new Mock<IConferenceApiService>();
            service.Setup(m => m.FindSpeaker(It.IsAny<string>())).ReturnsAsync(speakerSearchResult);
            service.Setup(m => m.ReadSessionsWithTopics(It.IsAny<Item>(), It.IsAny<DateTime>())).ReturnsAsync(speakerResult);

            return service.Object;
        }

        public static ConferenceApiService BuildService(StringContent response, HttpStatusCode statusCode)
        {
            var opts = Initialiser.ConferenceApiConfig();
            var clientFactory = Initialiser.ConferenceClientFactory(response,
                statusCode);
            var sut = new ConferenceApiService(opts, clientFactory);
            return sut;
        }

        public static IOptions<ConferenceApi> ConferenceApiConfig()
        {
            return Options.Create(new ConferenceApi { AuthKey = "123123", ApiBaseUrl = @"https://apicandidates.azure-api.net/conference/" });
        }
    }
}
