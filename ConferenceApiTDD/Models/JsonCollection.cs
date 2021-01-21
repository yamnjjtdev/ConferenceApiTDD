using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApiTDD.Models
{
    public class Datum
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class Item
    {
        public string href { get; set; }
        public List<Datum> data { get; set; }
        public List<Link> links { get; set; }
    }

    public class Template
    {
        public List<object> data { get; set; }
    }

    public class Collection
    {
        public string version { get; set; }
        public string href { get; set; }
        public List<object> links { get; set; }
        public List<Item> items { get; set; }
        public List<object> queries { get; set; }
        public Template template { get; set; }
    }

    public class JsonCollection
    {
        public Collection collection { get; set; }
    }


}
