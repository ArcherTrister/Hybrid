using System;
using System.ComponentModel.DataAnnotations;

namespace Hybrid.Core.Configuration
{
    /// <summary>
    /// Quartz配置选项
    /// </summary>
    public sealed class QuartzConfiguration : IQuartzConfiguration
    {
        /// <summary>
        /// 调度名称/实例名称
        /// </summary>
        public string SchedulerName { get; set; }

        /// <summary>
        /// 线程数
        /// </summary>
        [Range(1, 100, ErrorMessage = "线程数设置错误，范围1-100")]
        public int ThreadCount { get; set; } = 1;

        /// <summary>
        /// 作业中每个线程的优先级
        /// </summary>
        [Range(0, 4, ErrorMessage = "线程的优先级设置错误，范围0-4")]
        public System.Threading.ThreadPriority ThreadPriority { get; set; } = System.Threading.ThreadPriority.Normal;

        /// <summary>
        /// 存储类型
        /// </summary>
        [Range(0, 2, ErrorMessage = "存储类型设置错误，范围0-2")]
        public QuartzStorageType StorageType { get; set; } = QuartzStorageType.InMemory;

        /// <summary>
        /// 数据库连接字符串/内存缓存名称
        /// </summary>
        [Required(ErrorMessage = "数据库连接字符串或内存缓存名称不能为空")]
        public string ConnectionStringOrCacheName { get; set; }

        /// <summary>
        /// SqlServer表前缀
        /// </summary>
        public string TablePrefix { get; set; }

        /// <summary>
        /// 序列化类型
        /// </summary>
        [Range(0, 1, ErrorMessage = "序列化类型设置错误，范围0-1")]
        public QuartzSerializerType SerializerType { get; set; } = QuartzSerializerType.Binary;

        /// <summary>
        /// 是否使用历史记录插件
        /// </summary>
        public bool IsUseHistoryPlugin { get; set; }

        /// <summary>
        /// 是否使用集群模式
        /// </summary>
        public bool IsClustered { get; set; } = false;

        /// <summary>
        /// 是否使用集群模式
        /// </summary>
        public bool IsUseSelectWithLockSQL { get; set; } = false;

        ///// <summary>
        ///// 是否使用仪表板
        ///// </summary>
        //public bool UseDashboard { get; set; } = true;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}