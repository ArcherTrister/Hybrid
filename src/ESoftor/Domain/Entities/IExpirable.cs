// -----------------------------------------------------------------------
//  <copyright file="IExpirable.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

using System;

namespace ESoftor.Domain.Entities
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