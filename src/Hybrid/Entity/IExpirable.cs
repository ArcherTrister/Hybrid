// -----------------------------------------------------------------------
//  <copyright file="IExpirable.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-08-19 2:33</last-date>
// -----------------------------------------------------------------------

using System;


namespace Hybrid.Entity
{
    /// <summary>
    /// 定义可过期性，包含生效时间和过期时间
    /// </summary>
    public interface IExpirable
    {
        /// <summary>
        /// 获取或设置 生效时间
        /// </summary>
        DateTime? BeginTime { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        DateTime? EndTime { get; set; }
    }
}