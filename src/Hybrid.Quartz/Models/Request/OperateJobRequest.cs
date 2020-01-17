namespace Hybrid.Quartz.Models.Request
{
    /// <summary>
    /// Job操作请求模型
    /// </summary>
    public sealed class OperateJobRequest
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
    }
}