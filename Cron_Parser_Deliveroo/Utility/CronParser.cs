using System;
using System.Collections.Generic;
using System.Linq;
using Cron_Parser_Deliveroo.Model;

namespace Cron_Parser_Deliveroo.Utility
{
    public static class CronParser
    {
        public static CronResultObject Parse(string cronExpression)
        {
            CronResultObject result = new CronResultObject();
            string[] sections = cronExpression.Split(' ');

            if (sections.Length != 5)
            {
                throw new ArgumentException($"{cronExpression} is not a valid input");
            }

            string minuteExpression = sections[0];
            string hourExpression = sections[1];
            string dayOfMonthExpression = sections[2];
            string monthExpression = sections[3];
            string dayOfWeekExpression = sections[4];

            result.Minutes = ParseSection(minuteExpression, CronSectionType.Minute);
            result.Hours = ParseSection(hourExpression, CronSectionType.Hour);
            result.DayOfMonth = ParseSection(dayOfMonthExpression, CronSectionType.DayOfMonth);
            result.Month = ParseSection(monthExpression, CronSectionType.Month);
            result.DayOfWeek = ParseSection(dayOfWeekExpression, CronSectionType.DayOfWeek);

            return result;
        }

        private static List<string> ParseSection(string expression, CronSectionType type)
        {
            HashSet<int> result = new HashSet<int>();
            int[] parserLimit = ParserLimit.GetParserLimit(type);

            //Set netive numbers to determine whether it has been set or not
            int interval = -1;
            int min = -1;
            int max = -1;

            string[] steps = expression.Split(BaseCharacter.Or);

            for (int i = 0; i < steps.Length; i++)
            {
                var currentStep = steps[i];

                //Check whether there is interval
                string[] stepWithInterval = currentStep.Split(BaseCharacter.Interval);

                if (stepWithInterval.Length > 1)
                {
                    interval = IntParser.PraseCronInt(type, stepWithInterval[1]);
                    if (interval <= 0)
                    {
                        throw new InvalidOperationException($"{interval} is invalid in {type}");
                    }
                }

                //Check whether there is Range
                string[] stepWithRange = stepWithInterval[0].Split(BaseCharacter.Range);

                if (stepWithRange.Length > 1)
                {
                    min = IntParser.PraseCronInt(type, stepWithRange[0]);
                    max = IntParser.PraseCronInt(type, stepWithRange[1]);
                }
                else
                {
                    if (stepWithRange[0] == BaseCharacter.Any)
                    {
                        min = parserLimit[0];
                        max = parserLimit[1];
                    }
                    else
                    {
                        min = IntParser.PraseCronInt(type, stepWithRange[0]);
                    }
                }

                //This handles * condition as well
                //Max > 0 means there is a Range
                if (max > 0)
                {
                    CalculateTimeWithin(min, max, interval).ForEach(x => result.Add(x));
                }
                else if (max < 0)
                {
                    if (interval > 0)
                    {
                        CalculateTimeWithin(min, parserLimit[1], interval).ForEach(x => result.Add(x));
                    }
                    else
                    {
                        CalculateTimeWithin(min, min, interval).ForEach(x => result.Add(x));
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Step: {currentStep} can not be handled");
                }
            }

            //Order Result
            List<int> orderedResult = result.OrderBy(x => x).ToList();
            List<string> orderedStringResult = orderedResult.Select(x => x.ToString()).ToList();
            return orderedStringResult;
        }

        private static List<int> CalculateTimeWithin(int min, int max, int interval)
        {
            List<int> result = new List<int>();
            //If interval is not set then default it to 1
            interval = Math.Max(interval, 1);

            while (min <= max)
            {
                result.Add(min);
                min += interval;
            }

            return result;
        }



    }
}
