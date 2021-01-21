using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using ConferenceApiTDD.Lib;
using ConferenceApiTDD.Models;

namespace ConferenceApiTDD.Test.Init
{
    public static class ConferenceApiResponses
    {
        public static StringContent OkResponse => BuildOkResponse();
        public static StringContent SpeakersResponse => new StringContent(SampleData.Speakers);
        public static StringContent SessionsResponse => new StringContent(SampleData.Sessions);
        public static StringContent TopicsResponse => new StringContent(SampleData.Topics);

        private static StringContent BuildOkResponse()
        {
            return new StringContent(SampleData.Speakers);
        }

    }
}
