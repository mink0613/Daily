using System;

namespace Daily.Common
{
    public class DateHelper
    {
        private static readonly int _periodStartDate = 16;

        private static  readonly int _periodEndDate = 15;

        public static DateTime GetDayOfWeek(DateTime date, DayOfWeek day)
        {
            if (date.DayOfWeek < day)
            {
                return GetDayOfWeek(date.AddDays(1), day);
            }
            else if (date.DayOfWeek > day)
            {
                // Let Sunday be the last day of week
                if (day == DayOfWeek.Sunday)
                {
                    return GetDayOfWeek(date.AddDays(1), day);
                }
                else
                {
                    return GetDayOfWeek(date.AddDays(-1), day);
                }
            }
            return date;
        }

        public static DateTime GetMondayOfWeek(DateTime date)
        {
            if (date.DayOfWeek != DayOfWeek.Monday)
            {
                return GetMondayOfWeek(date.AddDays(-1));
            }

            return date;
        }

        public static DateTime GetSundayOfWeek(DateTime date)
        {
            if (date.DayOfWeek != DayOfWeek.Sunday)
            {
                return GetSundayOfWeek(date.AddDays(1));
            }

            return date;
        }

        public static DateTime GetStartDayofPeriod(DateTime date)
        {
            if (date.Day != _periodStartDate)
            {
                return GetStartDayofPeriod(date.AddDays(-1));
            }

            return date;
        }

        public static DateTime GetEndDayofPeriod(DateTime date)
        {
            if (date.Day != _periodEndDate)
            {
                return GetEndDayofPeriod(date.AddDays(1));
            }

            return date;
        }

        public static string ToStringDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        public static string ToStringDateMMDD(DateTime date)
        {
            return date.ToString("MM-dd");
        }
    }
}
