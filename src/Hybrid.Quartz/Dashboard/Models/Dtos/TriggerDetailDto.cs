using Quartz;
using Quartz.Spi;
using Quartz.Util;

using System;
using System.Collections.Generic;

namespace Hybrid.Quartz.Dashboard.Models.Dtos
{
    public class TriggerDetailDto
    {
        protected TriggerDetailDto(ITrigger trigger, ICalendar calendar)
        {
            Description = trigger.Description;
            TriggerType = trigger.GetType().AssemblyQualifiedNameWithoutVersion();
            Name = trigger.Key.Name;
            Group = trigger.Key.Group;
            CalendarName = trigger.CalendarName;
            Priority = trigger.Priority;
            StartTimeUtc = trigger.StartTimeUtc;
            EndTimeUtc = trigger.EndTimeUtc;
            NextFireTimes = TriggerUtils.ComputeFireTimes((IOperableTrigger)trigger, calendar, 10);
        }

        public string Name { get; set; }
        public string Group { get; set; }
        public string TriggerType { get; set; }
        public string Description { get; set; }

        public string CalendarName { get; set; }
        public int Priority { get; set; }
        public DateTimeOffset StartTimeUtc { get; set; }
        public DateTimeOffset? EndTimeUtc { get; set; }

        public IReadOnlyList<DateTimeOffset> NextFireTimes { get; set; }

        public static TriggerDetailDto Create(ITrigger trigger, ICalendar calendar)
        {
            switch (trigger)
            {
                case ISimpleTrigger simpleTrigger:
                    return new SimpleTriggerDetailDto(simpleTrigger, calendar);

                case ICronTrigger cronTrigger:
                    return new CronTriggerDetailDto(cronTrigger, calendar);

                case ICalendarIntervalTrigger calendarIntervalTrigger:
                    return new CalendarIntervalTriggerDetailDto(calendarIntervalTrigger, calendar);

                case IDailyTimeIntervalTrigger dailyTimeIntervalTrigger:
                    return new DailyTimeIntervalTriggerDetailDto(dailyTimeIntervalTrigger, calendar);

                default:
                    return new TriggerDetailDto(trigger, calendar);
            }
        }

        public class CronTriggerDetailDto : TriggerDetailDto
        {
            public CronTriggerDetailDto(ICronTrigger trigger, ICalendar calendar) : base(trigger, calendar)
            {
                CronExpression = trigger.CronExpressionString;
                TimeZone = new TimeZoneDto(trigger.TimeZone);
            }

            public string CronExpression { get; }
            public TimeZoneDto TimeZone { get; }
        }

        public class SimpleTriggerDetailDto : TriggerDetailDto
        {
            public SimpleTriggerDetailDto(ISimpleTrigger trigger, ICalendar calendar) : base(trigger, calendar)
            {
                RepeatCount = trigger.RepeatCount;
                RepeatInterval = trigger.RepeatInterval;
                TimesTriggered = trigger.TimesTriggered;
            }

            public TimeSpan RepeatInterval { get; }
            public int RepeatCount { get; }
            public int TimesTriggered { get; }
        }

        public class CalendarIntervalTriggerDetailDto : TriggerDetailDto
        {
            public CalendarIntervalTriggerDetailDto(ICalendarIntervalTrigger trigger, ICalendar calendar) : base(trigger, calendar)
            {
                RepeatInterval = trigger.RepeatInterval;
                TimesTriggered = trigger.TimesTriggered;
                RepeatIntervalUnit = trigger.RepeatIntervalUnit;
                PreserveHourOfDayAcrossDaylightSavings = trigger.PreserveHourOfDayAcrossDaylightSavings;
                TimeZone = new TimeZoneDto(trigger.TimeZone);
                SkipDayIfHourDoesNotExist = trigger.SkipDayIfHourDoesNotExist;
            }

            public TimeZoneDto TimeZone { get; }
            public bool SkipDayIfHourDoesNotExist { get; }
            public bool PreserveHourOfDayAcrossDaylightSavings { get; }
            public IntervalUnit RepeatIntervalUnit { get; }
            public int RepeatInterval { get; }
            public int TimesTriggered { get; }
        }

        public class DailyTimeIntervalTriggerDetailDto : TriggerDetailDto
        {
            public DailyTimeIntervalTriggerDetailDto(IDailyTimeIntervalTrigger trigger, ICalendar calendar) : base(trigger, calendar)
            {
                RepeatInterval = trigger.RepeatInterval;
                TimesTriggered = trigger.TimesTriggered;
                RepeatIntervalUnit = trigger.RepeatIntervalUnit;
                TimeZone = new TimeZoneDto(trigger.TimeZone);
            }

            public TimeZoneDto TimeZone { get; }
            public IntervalUnit RepeatIntervalUnit { get; }
            public int TimesTriggered { get; }
            public int RepeatInterval { get; }
        }
    }
}