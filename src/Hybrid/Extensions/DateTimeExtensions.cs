// -----------------------------------------------------------------------
//  <copyright file="DateTimeExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-12-30 22:24</last-date>
// -----------------------------------------------------------------------

using System;
using System.Globalization;

namespace Hybrid.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToDefaultFormat(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
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