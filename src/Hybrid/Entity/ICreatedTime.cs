// -----------------------------------------------------------------------
//  <copyright file="ICreatedTime.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-08-18 14:11</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;

namespace Hybrid.Entity
{
    /// <summary>
    /// 定义创建时间
    /// </summary>
    public interface ICreatedTime
    {
        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        DateTime CreatedTime { get; set; }
    }
}