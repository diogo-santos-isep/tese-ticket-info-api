namespace Infrastructure.CrossCutting.Helpers
{
    using System;
    public static class DateTimeComparer
    {
        public static bool IsEqualDateTime(this DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year
                && date1.Month == date2.Month
                && date1.Day == date2.Day
                && date1.Hour == date2.Hour
                && date1.Minute == date2.Minute
                && date1.Second == date2.Second;
        }
    }
}
