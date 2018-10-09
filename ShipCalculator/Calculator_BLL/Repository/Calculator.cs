using System;
using Calculator_BLL.Helpers;


namespace Calculator_BLL.Repository
{
   public class Calculator
    {
        public string CalculateStops(string mglt, string consumables, int distance)
        {
            if (mglt == null || mglt.Equals("unknown") || consumables == null || consumables.Equals("unknown"))
            {
                return "unknown";
            }

            var dataConsumables = consumables.Split(' ');
            var typeOfTimes = dataConsumables[1];

            var time = double.Parse(dataConsumables[0]);
            var mglt_value = double.Parse(mglt);

            double countStop = 0;

            switch (typeOfTimes.ToLower())
            {
                case "years":
                case "year":
                    countStop = distance / (time * Constants.daysPerYear * Constants.hoursOnDay * mglt_value);
                    break;
                case "months":
                case "month":
                    countStop = distance / (time * Constants.daysPerMonth * Constants.hoursOnDay * mglt_value);
                    break;
                case "weeks":
                case "week":
                    countStop = distance / (time * Constants.daysPerWeek * Constants.hoursOnDay * mglt_value);
                    break;
                case "days":
                case "day":
                    countStop = distance / (time * Constants.hoursOnDay * mglt_value);
                    break;
                default:
                    break;
            }

            return Math.Round(countStop).ToString();

        }
    }
}
