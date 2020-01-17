using Quartz;
using Quartz.Impl.Calendar;
using Quartz.Util;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hybrid.Quartz.Models.Dtos
{
    public class CalendarDetailDto
    {
        protected CalendarDetailDto(ICalendar calendar, string calendarName)
        {
            Name = calendarName;
            CalendarType = calendar.GetType().AssemblyQualifiedNameWithoutVersion();
            Description = calendar.Description;
            if (calendar.CalendarBase != null)
            {
                CalendarBase = Create(calendar.CalendarBase, calendarName);
            }
        }

        public string Name { get; }

        public string CalendarType { get; }
        public string Description { get; }
        public CalendarDetailDto CalendarBase { get; }

        public static CalendarDetailDto Create(ICalendar calendar, string calendarName)
        {
            var annualCalendar = calendar as AnnualCalendar;
            if (annualCalendar != null)
            {
                return new AnnualCalendarDto(annualCalendar, calendarName);
            }

            var cronCalendar = calendar as CronCalendar;
            if (cronCalendar != null)
            {
                return new CronCalendarDto(cronCalendar, calendarName);
            }

            var dailyCalendar = calendar as DailyCalendar;
            if (dailyCalendar != null)
            {
                return new DailyCalendarDto(dailyCalendar, calendarName);
            }

            var holidayCalendar = calendar as HolidayCalendar;
            if (holidayCalendar != null)
            {
                return new HolidayCalendarDto(holidayCalendar, calendarName);
            }

            var monthlyCalendar = calendar as MonthlyCalendar;
            if (monthlyCalendar != null)
            {
                return new MonthlyCalendarDto(monthlyCalendar, calendarName);
            }

            var weeklyCalendar = calendar as WeeklyCalendar;
            if (weeklyCalendar != null)
            {
                return new WeeklyCalendarDto(weeklyCalendar, calendarName);
            }

            return new CalendarDetailDto(calendar, calendarName);
        }

        public class AnnualCalendarDto : CalendarDetailDto
        {
            public AnnualCalendarDto(AnnualCalendar calendar, string calendarName) : base(calendar, calendarName)
            {
                DaysExcluded = calendar.DaysExcluded;
                TimeZone = new TimeZoneDto(calendar.TimeZone);
            }

            public IReadOnlyCollection<DateTime> DaysExcluded { get; }
            public TimeZoneDto TimeZone { get; }
        }

        public class CronCalendarDto : CalendarDetailDto
        {
            public CronCalendarDto(CronCalendar calendar, string calendarName) : base(calendar, calendarName)
            {
                CronExpression = calendar.CronExpression.CronExpressionString;
                TimeZone = new TimeZoneDto(calendar.TimeZone);
            }

            public string CronExpression { get; }
            public TimeZoneDto TimeZone { get; }
        }

        public class DailyCalendarDto : CalendarDetailDto
        {
            public DailyCalendarDto(DailyCalendar calendar, string calendarName) : base(calendar, calendarName)
            {
                InvertTimeRange = calendar.InvertTimeRange;
                TimeZone = new TimeZoneDto(calendar.TimeZone);
            }

            public bool InvertTimeRange { get; }
            public TimeZoneDto TimeZone { get; }
        }

        public class HolidayCalendarDto : CalendarDetailDto
        {
            public HolidayCalendarDto(HolidayCalendar calendar, string calendarName) : base(calendar, calendarName)
            {
                ExcludedDates = calendar.ExcludedDates.ToList();
                TimeZone = new TimeZoneDto(calendar.TimeZone);
            }

            public IReadOnlyList<DateTime> ExcludedDates { get; }
            public TimeZoneDto TimeZone { get; }
        }

        public class MonthlyCalendarDto : CalendarDetailDto
        {
            public MonthlyCalendarDto(MonthlyCalendar calendar, string calendarName) : base(calendar, calendarName)
            {
                DaysExcluded = calendar.DaysExcluded.ToList();
                TimeZone = new TimeZoneDto(calendar.TimeZone);
            }

            public IReadOnlyList<bool> DaysExcluded { get; }
            public TimeZoneDto TimeZone { get; }
        }

        public class WeeklyCalendarDto : CalendarDetailDto
        {
            public WeeklyCalendarDto(WeeklyCalendar calendar, string calendarName) : base(calendar, calendarName)
            {
                DaysExcluded = calendar.DaysExcluded.ToList();
                TimeZone = new TimeZoneDto(calendar.TimeZone);
            }

            public IReadOnlyList<bool> DaysExcluded { get; }
            public TimeZoneDto TimeZone { get; }
        }
    }
}