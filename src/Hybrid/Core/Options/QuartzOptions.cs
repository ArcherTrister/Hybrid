// -----------------------------------------------------------------------
//  <copyright file="QuartzOptions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-04-29 3:52</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Core.Options
{
    /// <summary>
    /// Quartz配置选项
    /// </summary>
    public sealed class QuartzOptions
    {
        /// <summary>
        /// 调度名称
        /// </summary>
        public string SchedulerName { get; set; }

        /// <summary>
        /// 存储类型
        /// </summary>
        public QuartzStorageType StorageType { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// SqlServer表前缀
        /// </summary>
        public string TablePrefix { get; set; }

        /// <summary>
        /// 序列化类型
        /// </summary>
        public QuartzSerializerType SerializerType { get; set; }

        /// <summary>
        /// 是否使用历史记录插件
        /// </summary>
        public bool IsUseHistoryPlugin { get; set; }

        /// <summary>
        /// 是否使用仪表板
        /// </summary>
        public bool UseDashboard { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
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