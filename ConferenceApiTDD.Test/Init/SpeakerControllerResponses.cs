using System;
using System.Collections.Generic;
using System.Text;
using ConferenceApiTDD.Models;

namespace ConferenceApiTDD.Test.Init
{
    public static class SpeakerControllerResponses
    {

        public static Item FoundSpeakerJsonResponse(string speakerName) => new Item() { data = new List<Datum> { new Datum() { name = "Name", value = speakerName } } };
        public static Item NullSpeakerJsonResponse => null;
        public static Speaker SpeakerResponse(string speakerName) => new Speaker() { Name = speakerName };
    }
}
