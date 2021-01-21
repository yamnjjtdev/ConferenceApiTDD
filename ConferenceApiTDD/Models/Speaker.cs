using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApiTDD.Models
{
    public class Speaker
    {
        public string Name { get; set; }
        public virtual List<Session> Sessions { get; set; }
    }
}
