using Quartz;

using System.Collections.Generic;

namespace Hybrid.Quartz.Dashboard.Models.Dtos
{
    public sealed class DashboardDto
    {
        public Histogram History { get; set; }
        public SchedulerMetaData MetaData { get; set; }
        public string RunningSince { get; set; }
        public string MachineName { get; set; }
        public string CommandLine { get; set; }
        public int JobsCount { get; set; }
        public int TriggerCount { get; set; }
        public int ExecutingJobs { get; set; }
        public long ExecutedJobs { get; set; }
        public long FailedJobs { get; set; }
        public long TotalJobs { get; set; }
        public IEnumerable<object> JobGroups { get; set; }
        public IEnumerable<object> TriggerGroups { get; set; }
        public bool HistoryEnabled { get; set; }
    }
}