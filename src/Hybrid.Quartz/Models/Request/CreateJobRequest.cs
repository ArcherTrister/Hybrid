namespace Hybrid.Quartz.Models.Request
{
    /// <summary>
    /// 创建任务请求模型
    /// </summary>
    public sealed class CreateJobRequest
    {
        /// <summary>
        /// 调度名称
        /// </summary>
        public string SchedulerName { get; set; }

        /// <summary>
        /// 任务分组
        /// </summary>
        public string JobGroup { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public string JobType { get; set; }

        /// <summary>
        /// 是否是持久化
        /// </summary>
        public bool Durable { get; set; }

        /// <summary>
        /// 是否恢复请求
        /// </summary>
        public bool RequestsRecovery { get; set; }

        /// <summary>
        /// 是否替换
        /// </summary>
        public bool Replace { get; set; }
    }
}