using System;
using System.Collections.Generic;

namespace Cron_Parser_Deliveroo.Model
{
    public class CronResultObject
    {
        public List<string> Minutes;
        public List<string> Hours;
        public List<string> DayOfMonth;
        public List<string> DayOfWeek;
        public List<string> Month;

        public CronResultObject()
        {
            Minutes = new List<string>();
            Hours = new List<string>();
            DayOfMonth = new List<string>();
            DayOfWeek = new List<string>();
            Month = new List<string>();
        }

        public void PrintSelf()
        {
            Console.WriteLine("minute        " + string.Join(',', Minutes));
            Console.WriteLine("hour          " + string.Join(',', Hours));
            Console.WriteLine("day of month  " + string.Join(',', DayOfMonth));
            Console.WriteLine("month         " + string.Join(',', Month));
            Console.WriteLine("dayofweek     " + string.Join(',', DayOfWeek));
        }
    }
}
