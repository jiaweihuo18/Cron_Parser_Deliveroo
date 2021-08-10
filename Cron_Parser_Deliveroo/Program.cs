using System;
using Cron_Parser_Deliveroo.Model;
using Cron_Parser_Deliveroo.Utility;

namespace Cron_Parser_Deliveroo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CronResultObject result = CronParser.Parse(args[0]);
            result.PrintSelf();
            Console.ReadKey();
        }
    }
}
