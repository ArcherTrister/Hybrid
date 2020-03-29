using Hybrid.Data;
using System;
using System.Globalization;

namespace Hybrid.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 時間默認格式化
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDefaultFormat(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 将JS时间格式的数值转换为时间
        /// </summary>
        public static DateTime FromJsGetTime(this long jsTime)
        {
            int length = jsTime.ToString().Length;
            Check.Required<ArgumentException>(length != 10 || length != 13, "JS时间数值的长度不正确，必须为10位或13位");
            DateTime start = new DateTime(1970, 1, 1);
            DateTime result = length == 10 ? start.AddSeconds(jsTime) : start.AddMilliseconds(jsTime);
            return result.FromUtcTime();
        }

        /// <summary>
        /// 将当前时区时间转换为UTC时间
        /// </summary>
        public static DateTime ToUtcTime(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Local);
        }

        /// <summary>
        /// 将指定UTC时间转换为当前时区的时间
        /// </summary>
        public static DateTime FromUtcTime(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.Local);
        }

        public static string ToNiceFormat(this TimeSpan? timeSpan)
        {
            if (timeSpan == null) return "";

            var ts = timeSpan.Value;

            if (ts.TotalSeconds < 1)
                return (int)ts.TotalMilliseconds + "ms";

            if (ts.TotalMinutes < 1)
                return (int)ts.TotalSeconds + " seconds";

            if (ts.TotalHours < 1)
                return (int)ts.TotalMinutes + " minutes";

            if (ts.TotalDays < 1)
                return string.Format(CultureInfo.InvariantCulture, "{0:hh\\:mm}", timeSpan);

            return string.Format(CultureInfo.InvariantCulture, "{0:%d} days {0:hh\\:mm}", timeSpan);
        }
    }
}
