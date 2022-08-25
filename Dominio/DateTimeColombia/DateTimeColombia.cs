using TimeZoneConverter;

namespace Dominio.DateTimeColombia
{
    public static class DateTimeColombia
    {
        public static TimeZoneInfo ColombiaTimeZone { get; } = TZConvert.GetTimeZoneInfo("SA Pacific Standard Time");

        public static DateTime Now() => GetDate(DateTime.UtcNow);
        public static DateTime Today() => Now().Date;


        public static DateTimeOffset NowOffset() => GetDateTimeOffset(DateTimeOffset.UtcNow);
        public static DateTimeOffset TodayOffset() => NowOffset().Date;

        /// <summary>
        /// Obtiene fecha y hora para Colombia desde una fecha con los datos en UTC
        /// </summary>
        /// <param name="newDate"></param>
        /// <returns></returns>
        public static DateTime GetDate(DateTime newDate)
        {
            var getTime = TimeZoneInfo.ConvertTimeFromUtc(newDate, ColombiaTimeZone);
            return getTime;
        }

        /// <summary>
        /// Obtiene fecha (Datetimeoffset) para Colombia
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static DateTimeOffset GetDateTimeOffset(DateTimeOffset original)
        {
            var cetTime = TimeZoneInfo.ConvertTime(original, ColombiaTimeZone);
            return cetTime;
        }
    }
}
