using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApiTDD.Models
{
    public class Session
    {
        public string Title { get; set; }
        public string Timeslot { get; set; }
        public virtual List<Topic> Topics { get; set; }
    }
}
