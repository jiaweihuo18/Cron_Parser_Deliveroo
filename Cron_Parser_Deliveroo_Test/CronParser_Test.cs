using System;
using System.Collections.Generic;
using System.Linq;
using Cron_Parser_Deliveroo.Model;
using Cron_Parser_Deliveroo.Utility;
using Xunit;

namespace Cron_Parser_Deliveroo_Test
{
    public class CronParser_Test
    {

        [Fact]
        public void Test_Point_Time()
        {
            string cronExpression = "0 4 8 2 3";

            CronResultObject result = CronParser.Parse(cronExpression);

            Assert.Equal(result.Minutes, new List<string> { "0" });
            Assert.Equal(result.Hours, new List<string> { "4" });
            Assert.Equal(result.DayOfMonth, new List<string> { "8" });
            Assert.Equal(result.Month, new List<string> { "2" });
            Assert.Equal(result.DayOfWeek, new List<string> { "3" });
        }

        [Fact]
        public void Test_Range()
        {
            string cronExpression = "0 4 8-14 * *";

            CronResultObject result = CronParser.Parse(cronExpression);

            Assert.Equal(result.Minutes, new List<string> { "0" });
            Assert.Equal(result.Hours, new List<string> { "4" });
            Assert.Equal(result.DayOfMonth, new List<string> { "8", "9", "10", "11", "12", "13", "14" });
            Assert.Equal(result.Month, new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
            Assert.Equal(result.DayOfWeek, new List<string> { "0", "1", "2", "3", "4", "5", "6" });
        }

        [Fact]
        public void Test_Interval()
        {
            string cronExpression = "23 2/22 2 * *";

            CronResultObject result = CronParser.Parse(cronExpression);

            Assert.Equal(result.Minutes, new List<string> { "23" });
            Assert.Equal(result.Hours, new List<string> { "2" });
            Assert.Equal(result.DayOfMonth, new List<string> { "2" });
            Assert.Equal(result.Month, new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
            Assert.Equal(result.DayOfWeek, new List<string> { "0", "1", "2", "3", "4", "5", "6" });
        }

        [Fact]
        public void Test_Range_With_Interval()
        {
            string cronExpression = "23 0-20/2 2 * *";

            CronResultObject result = CronParser.Parse(cronExpression);

            Assert.Equal(result.Minutes, new List<string> { "23" });
            Assert.Equal(result.Hours, new List<string> { "0", "2", "4", "6", "8", "10", "12", "14", "16", "18", "20" });
            Assert.Equal(result.DayOfMonth, new List<string> { "2" });
            Assert.Equal(result.Month, new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
            Assert.Equal(result.DayOfWeek, new List<string> { "0", "1", "2", "3", "4", "5", "6" });
        }

        [Fact]
        public void Test_Any_With_Interval()
        {
            string cronExpression = "23 */22 2 * *";

            CronResultObject result = CronParser.Parse(cronExpression);

            Assert.Equal(result.Minutes, new List<string> { "23" });
            Assert.Equal(result.Hours, new List<string> { "0", "22" });
            Assert.Equal(result.DayOfMonth, new List<string> { "2" });
            Assert.Equal(result.Month, new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
            Assert.Equal(result.DayOfWeek, new List<string> { "0", "1", "2", "3", "4", "5", "6" });
        }

        [Fact]
        public void Test_Minute_OutOfRange()
        {
            string cronExpression = "60 2/22 2 * *";

            var ex = Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
            Assert.Equal(ex.Message, $"60 is out of range of {CronSectionType.Minute}");

            cronExpression = "-1-2 2/22 2 * *";

            ex = Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
            Assert.Equal(ex.Message, $"Invalid int input: {string.Empty}");
        }

        [Fact]
        public void Test_Hour_OutOfRange()
        {
            string cronExpression = "22 24 2 * *";

            var ex = Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
            Assert.Equal(ex.Message, $"24 is out of range of {CronSectionType.Hour}");

            cronExpression = "22 -1 2 * *";

            ex = Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
            Assert.Equal(ex.Message, $"Invalid int input: {string.Empty}");
        }

        [Fact]
        public void Test_DayOfMonth_OutOfRange()
        {
            string cronExpression = "22 23 32 * *";

            var ex = Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
            Assert.Equal(ex.Message, $"32 is out of range of {CronSectionType.DayOfMonth}");

            cronExpression = "22 23 0 * *";

            ex = Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
            Assert.Equal(ex.Message, $"0 is out of range of {CronSectionType.DayOfMonth}");

            cronExpression = "22 23 -1 * *";

            ex = Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
            Assert.Equal(ex.Message, $"Invalid int input: {string.Empty}");
        }

        [Fact]
        public void Test_Month_OutOfRange()
        {
            string cronExpression = "22 23 31 13 *";

            var ex = Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
            Assert.Equal(ex.Message, $"13 is out of range of {CronSectionType.Month}");

            cronExpression = "22 23 31 0 *";

            ex = Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
            Assert.Equal(ex.Message, $"0 is out of range of {CronSectionType.Month}");

            cronExpression = "22 23 31 -1 *";

            ex = Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
            Assert.Equal(ex.Message, $"Invalid int input: {string.Empty}");
        }


        [Fact]
        public void Test_DayOfWeek_OutOfRange()
        {
            string cronExpression = "22 23 31 12 7";

            var ex = Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
            Assert.Equal(ex.Message, $"7 is out of range of {CronSectionType.DayOfWeek}");

            cronExpression = "22 23 31 12 -1";
            ex = Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
            Assert.Equal(ex.Message, $"Invalid int input: {string.Empty}");
        }

        [Fact]
        public void Test_Interval_Negtive()
        {
            string cronExpression = "2/-1 23 31 12 6";

            var ex = Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
            Assert.Equal(ex.Message, $"-1 is out of range of {CronSectionType.Minute}");
        }

        [Fact]
        public void Test_Interval_Zero()
        {
            string cronExpression = "2/0 23 31 12 6";

            var ex = Assert.Throws<InvalidOperationException>(() => CronParser.Parse(cronExpression));
            Assert.Equal(ex.Message, $"0 is invalid in {CronSectionType.Minute}");
        }

        [Fact]
        public void Test_Empty_Week()
        {
            string cronExpression = "2/0 23 31 12";

            var ex = Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
            Assert.Equal(ex.Message, $"{cronExpression} is not a valid input");
        }
    }
}
