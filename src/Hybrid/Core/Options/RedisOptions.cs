// -----------------------------------------------------------------------
//  <copyright file="RedisOptions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-12-14 17:00</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Core.Options
{
    /// <summary>
    /// Redis选项
    /// </summary>
    public class RedisOptions
    {
        /// <summary>
        /// 获取或设置 Redis连接配置
        /// </summary>
        public string Configuration { get; set; }

        /// <summary>
        /// 获取或设置 Redis实例名称
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// 获取或设置 是否启用
        /// </summary>
        public bool Enabled { get; set; }
    }
}