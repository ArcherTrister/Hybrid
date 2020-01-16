using Quartz.Impl.AdoJobStore;

namespace Hybrid.Quartz.SqlServer
{
    public class SqlServerQuartzOptions
    {
        /// <summary>
        /// 调度名称
        /// </summary>
        public string SchedulerName { get; set; } = QuartzConsts.DefaultSchedulerName;

        /// <summary>
        /// SqlServer数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// SqlServer表前缀
        /// </summary>
        public string TablePrefix { get; set; } = AdoConstants.DefaultTablePrefix;

        /// <summary>
        /// 序列化类型
        /// </summary>
        public QuartzSerializerType SerializerType { get; set; } = QuartzSerializerType.Binary;

        /// <summary>
        /// 是否使用历史记录插件
        /// </summary>
        public bool IsUseHistoryPlugin { get; set; } = true;
    }
}