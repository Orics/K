using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHAT_WPF.Utilities
{
    class KDateTime
    {
        public static string ConverToTimeDisplay(DateTime time) {
            TimeSpan timespan = DateTime.Now - time;
            if(timespan.TotalSeconds < 60)
            {
                return ((int)timespan.TotalSeconds).ToString() + "s";
            }
            else
            if(timespan.TotalMinutes < 60)
            {
                return ((int)timespan.TotalMinutes).ToString() + "m";
            }
            else
            if(timespan.TotalHours < 24)
            {
                return ((int)timespan.TotalHours).ToString() + "h";
            }
            else
            {
                return ((int)timespan.TotalDays).ToString() + "day";
            }
        }
    }
}
