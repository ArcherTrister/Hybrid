using Hybrid.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hybrid.Quartz.Plugins.History
{
    [Serializable]
    public sealed class ExecutionHistoryEntry // : Entity<long>
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public override long Id { get; set; }

        public string JobGroup { get; set; }
        public string TriggerGroup { get; set; }
        public DateTimeOffset FiredTime { get; set; }
        public DateTimeOffset ScheduledTime { get; set; }
        public TimeSpan RunTime { get; set; }
        public bool Error { get; set; }

        public string FireInstanceId { get; set; }
        public string SchedulerInstanceId { get; set; }
        public string SchedulerName { get; set; }
        public string JobName { get; set; }
        public string TriggerName { get; set; }
        public DateTime? ScheduledFireTimeUtc { get; set; }
        public DateTime ActualFireTimeUtc { get; set; }

        /// <summary>
        /// 是否恢复中
        /// </summary>
        public bool Recovering { get; set; }

        /// <summary>
        /// 执行是否被否决
        /// </summary>
        public bool Vetoed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? FinishedTimeUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
