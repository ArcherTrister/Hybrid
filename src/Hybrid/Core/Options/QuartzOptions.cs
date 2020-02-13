// -----------------------------------------------------------------------
//  <copyright file="QuartzOptions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-04-29 3:52</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;
using System.ComponentModel.DataAnnotations;

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
        public string SchedulerName { get; set; } = HybridConsts.DefaultSchedulerName;

        /// <summary>
        /// 存储类型
        /// </summary>
        [Range(0, 2, ErrorMessage = "存储类型设置错误")]
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
        [Range(0, 1, ErrorMessage = "序列化类型设置错误")]
        public QuartzSerializerType SerializerType { get; set; } = QuartzSerializerType.Binary;

        /// <summary>
        /// 是否使用历史记录插件
        /// </summary>
        public bool IsUseHistoryPlugin { get; set; }

        ///// <summary>
        ///// 是否使用仪表板
        ///// </summary>
        //public bool UseDashboard { get; set; } = true;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
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