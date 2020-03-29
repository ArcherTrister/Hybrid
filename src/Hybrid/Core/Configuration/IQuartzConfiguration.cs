using Hybrid.Domain.Entities;

namespace Hybrid.Core.Configuration
{
    /// <summary>
    /// Quartz配置选项
    /// </summary>
    public interface IQuartzConfiguration : IEnabled
    {
        /// <summary>
        /// 调度名称/实例名称
        /// </summary>
        string SchedulerName { get; set; }

        /// <summary>
        /// 线程数
        /// </summary>
        int ThreadCount { get; set; }

        /// <summary>
        /// 作业中每个线程的优先级
        /// </summary>

        System.Threading.ThreadPriority ThreadPriority { get; set; }

        /// <summary>
        /// 存储类型
        /// </summary>

        QuartzStorageType StorageType { get; set; }

        /// <summary>
        /// 数据库连接字符串/内存缓存名称
        /// </summary>

        string ConnectionStringOrCacheName { get; set; }

        /// <summary>
        /// SqlServer表前缀
        /// </summary>
        string TablePrefix { get; set; }

        /// <summary>
        /// 序列化类型
        /// </summary>

        QuartzSerializerType SerializerType { get; set; }

        /// <summary>
        /// 是否使用历史记录插件
        /// </summary>
        bool IsUseHistoryPlugin { get; set; }

        /// <summary>
        /// 是否使用集群模式
        /// </summary>
        bool IsClustered { get; set; }

        /// <summary>
        /// 是否使用集群模式
        /// </summary>
        bool IsUseSelectWithLockSQL { get; set; }

        ///// <summary>
        ///// 是否使用仪表板
        ///// </summary>
        //public bool UseDashboard { get; set; } = true;

        /// <summary>
        /// 是否启用
        /// </summary>
        bool IsEnabled { get; set; }
    }

    /// <summary>
    /// 序列化类型
    /// </summary>
    public enum QuartzSerializerType
    {
        /// <summary>
        /// 二进制
        /// </summary>
        Binary,

        /// <summary>
        /// Json
        /// </summary>
        Json
    }

    /// <summary>
    /// 存储类型
    /// </summary>
    public enum QuartzStorageType
    {
        /// <summary>
        /// 内存
        /// </summary>
        InMemory,

        /// <summary>
        /// SqlServer
        /// </summary>
        SqlServer,

        /// <summary>
        /// MySql
        /// </summary>
        MySql
    }
}