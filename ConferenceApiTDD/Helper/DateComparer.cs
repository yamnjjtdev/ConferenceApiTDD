using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConferenceApiTDD.Lib
{
    public static class DateComparer
    {
        public static bool IsDateTimeWithin(DateTime dateToCheck, string targetDatetime)
        {
            CultureInfo enUS = new CultureInfo("en-US");
            const string PatternDatetime = @"^(\d{2}\s\w*\s\d{4})\s(\d{2}:\d{2})\s-\s(\d{2}:\d{2})";
            const string exactFormat = "dd MMMM yyyy HH:mm";

            var result = Regex.Match(targetDatetime, PatternDatetime);
            if (result.Success && result.Groups.Count == 4 )
            {
                var startDatetime = DateTime.ParseExact($"{result.Groups[1].Value} {result.Groups[2].Value}", exactFormat, enUS, DateTimeStyles.None);
                var endDatetime = DateTime.ParseExact($"{result.Groups[1]} {result.Groups[3]}", exactFormat, enUS);

                return dateToCheck >= startDatetime && dateToCheck < endDatetime;
            }
            else
            {
                throw new FormatException($"Error parsing date time {targetDatetime}");
            }


        }

    }

}
