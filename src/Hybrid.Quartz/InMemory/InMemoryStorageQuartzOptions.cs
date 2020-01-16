namespace Hybrid.Quartz.InMemory
{
    public class InMemoryStorageQuartzOptions
    {
        /// <summary>
        /// 调度名称
        /// </summary>
        public string SchedulerName { get; set; } = QuartzConsts.DefaultSchedulerName;

        /// <summary>
        /// 是否使用历史记录插件
        /// </summary>
        public bool IsUseHistoryPlugin { get; set; } = false;

        ///// <summary>
        ///// 序列化类型
        ///// </summary>
        //public SerializerType SerializerType { get; set; } = SerializerType.Binary;
    }
}