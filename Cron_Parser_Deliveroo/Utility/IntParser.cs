using System;
using Cron_Parser_Deliveroo.Model;

namespace Cron_Parser_Deliveroo.Utility
{
    public static class IntParser
    {

        public static int PraseCronInt(CronSectionType type, string intString)
        {
            var parserLimit = ParserLimit.GetParserLimit(type);

            if (int.TryParse(intString, out int result))
            {
                if (result >= parserLimit[0] && result <= parserLimit[1])
                {
                    return result;
                }
                else
                {
                    throw new ArgumentException($"{intString} is out of range of {type}");
                }
            }
            else
            {
                throw new ArgumentException($"Invalid int input: {intString}");
            }
        }

    }
}
