using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceApiTDD.Models;

namespace ConferenceApiTDD.Lib
{
    public interface IConferenceApiService
    {
        Task<Item> FindSpeaker(string speakerName);
        Task<Speaker> ReadSessionsWithTopics(Item speaker, DateTime? datetime);
    }
}