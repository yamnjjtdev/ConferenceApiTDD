using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceApiTDD.Test.Init
{
    public static class SampleData
    {
        public static string Speakers = @"
{
   ""collection"":{
      ""version"":""1.0"",
      ""href"":""https://conferenceapi.azurewebsites.net:443/speakers"",
      ""links"":[
         
      ],
      ""items"":[
         {
            ""href"":""https://conferenceapi.azurewebsites.net/speaker/1"",
            ""data"":[
               {
                  ""name"":""Name"",
                  ""value"":""Scott Guthrie""
               }
            ],
            ""links"":[
               {
                  ""rel"":""http://tavis.net/rels/sessions"",
                  ""href"":""https://conferenceapi.azurewebsites.net/speaker/1/sessions""
               }
            ]
         },
         {
            ""href"":""https://conferenceapi.azurewebsites.net/speaker/2"",
            ""data"":[
               {
                  ""name"":""Name"",
                  ""value"":""Don Syme""
               }
            ],
            ""links"":[
               {
                  ""rel"":""http://tavis.net/rels/sessions"",
                  ""href"":""https://conferenceapi.azurewebsites.net/speaker/2/sessions""
               }
            ]
         }
      ]
   }
}
";
        public static string Sessions = @"
{
    ""collection"": {
        ""version"": ""1.0"",
        ""links"": [],
        ""items"": [
            {
                ""href"": ""https://conferenceapi.azurewebsites.net/session/114"",
                ""data"": [
                    {
                        ""name"": ""Title"",
                        ""value"": ""\r\n\t\t\tIntroduction to Windows Azure Part I\r\n\t\t""
                    },
                    {
                        ""name"": ""Timeslot"",
                        ""value"": ""04 December 2013 13:40 - 14:40""
                    },
                    {
                        ""name"": ""Speaker"",
                        ""value"": ""Scott Guthrie""
                    }
                ],
                ""links"": [
                    {
                        ""rel"": ""http://tavis.net/rels/speaker"",
                        ""href"": ""https://conferenceapi.azurewebsites.net/speaker/1""
                    },
                    {
                        ""rel"": ""http://tavis.net/rels/topics"",
                        ""href"": ""https://conferenceapi.azurewebsites.net/session/114/topics""
                    }
                ]
            },
            {
                ""href"": ""https://conferenceapi.azurewebsites.net/session/121"",
                ""data"": [
                    {
                        ""name"": ""Title"",
                        ""value"": ""\r\n\t\t\tIntroduction to Windows Azure Part II\r\n\t\t""
                    },
                    {
                        ""name"": ""Timeslot"",
                        ""value"": ""04 December 2013 15:00 - 16:00""
                    },
                    {
                        ""name"": ""Speaker"",
                        ""value"": ""Scott Guthrie""
                    }
                ],
                ""links"": [
                    {
                        ""rel"": ""http://tavis.net/rels/speaker"",
                        ""href"": ""https://conferenceapi.azurewebsites.net/speaker/1""
                    },
                    {
                        ""rel"": ""http://tavis.net/rels/topics"",
                        ""href"": ""https://conferenceapi.azurewebsites.net/session/121/topics""
                    }
                ]
            },
            {
                ""href"": ""https://conferenceapi.azurewebsites.net/session/200"",
                ""data"": [
                    {
                        ""name"": ""Title"",
                        ""value"": ""\r\n\t\t\tBuild Real World Cloud Apps using Windows Azure Part I\r\n\t\t""
                    },
                    {
                        ""name"": ""Timeslot"",
                        ""value"": ""05 December 2013 09:00 - 10:00""
                    },
                    {
                        ""name"": ""Speaker"",
                        ""value"": ""Scott Guthrie""
                    }
                ],
                ""links"": [
                    {
                        ""rel"": ""http://tavis.net/rels/speaker"",
                        ""href"": ""https://conferenceapi.azurewebsites.net/speaker/1""
                    },
                    {
                        ""rel"": ""http://tavis.net/rels/topics"",
                        ""href"": ""https://conferenceapi.azurewebsites.net/session/200/topics""
                    }
                ]
            },
            {
                ""href"": ""https://conferenceapi.azurewebsites.net/session/207"",
                ""data"": [
                    {
                        ""name"": ""Title"",
                        ""value"": ""\r\n\t\t\tBuild Real World Cloud Apps using Windows Azure Part II\r\n\t\t""
                    },
                    {
                        ""name"": ""Timeslot"",
                        ""value"": ""05 December 2013 10:20 - 11:20""
                    },
                    {
                        ""name"": ""Speaker"",
                        ""value"": ""Scott Guthrie""
                    }
                ],
                ""links"": [
                    {
                        ""rel"": ""http://tavis.net/rels/speaker"",
                        ""href"": ""https://conferenceapi.azurewebsites.net/speaker/1""
                    },
                    {
                        ""rel"": ""http://tavis.net/rels/topics"",
                        ""href"": ""https://conferenceapi.azurewebsites.net/session/207/topics""
                    }
                ]
            }
        ],
        ""queries"": [],
        ""template"": {
            ""data"": []
        }
    }
}
";
        public static string Topics = @"
{
    ""collection"": {
        ""version"": ""1.0"",
        ""links"": [],
        ""items"": [
            {
                ""href"": ""https://conferenceapi.azurewebsites.net/topic/11"",
                ""data"": [
                    {
                        ""name"": ""Title"",
                        ""value"": ""Programming Languages""
                    }
                ],
                ""links"": [
                    {
                        ""rel"": ""http://tavis.net/rels/sessions"",
                        ""href"": ""https://conferenceapi.azurewebsites.net/topic/11/sessions""
                    }
                ]
            },
            {
                ""href"": ""https://conferenceapi.azurewebsites.net/topic/17"",
                ""data"": [
                    {
                        ""name"": ""Title"",
                        ""value"": ""Web""
                    }
                ],
                ""links"": [
                    {
                        ""rel"": ""http://tavis.net/rels/sessions"",
                        ""href"": ""https://conferenceapi.azurewebsites.net/topic/17/sessions""
                    }
                ]
            }
        ],
        ""queries"": [],
        ""template"": {
            ""data"": []
        }
    }
}
";
    }
}
