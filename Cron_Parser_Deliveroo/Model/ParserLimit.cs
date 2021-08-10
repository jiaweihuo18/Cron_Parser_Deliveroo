using System;

namespace Cron_Parser_Deliveroo.Model
{
    public static class ParserLimit
    {
        public static readonly int[] Minute = { 0, 59 };
        public static readonly int[] Hour = { 0, 23 };
        public static readonly int[] DayOfMonth = { 1, 31 };
        public static readonly int[] Month = { 1, 12 };
        //Sunday is 0
        public static readonly int[] DayOfWeek = { 0, 6 };

        public static int[] GetParserLimit(CronSectionType section)
        {
            return section switch
            {
                CronSectionType.Minute => Minute,
                CronSectionType.Hour => Hour,
                CronSectionType.DayOfMonth => DayOfMonth,
                CronSectionType.Month => Month,
                CronSectionType.DayOfWeek => DayOfWeek,
                _ => throw new ArgumentException("Invalid Cron Section is found"),
            };
        }
    }
}
