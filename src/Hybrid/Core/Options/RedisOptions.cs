// -----------------------------------------------------------------------
//  <copyright file="RedisOptions.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-12-14 17:00</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Redis连接配置不能为空")]
        public string Configuration { get; set; }

        /// <summary>
        /// 获取或设置 Redis实例名称
        /// </summary>
        [Required(ErrorMessage = "Redis实例名称不能为空")]
        public string InstanceName { get; set; }

        /// <summary>
        /// 获取或设置 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}