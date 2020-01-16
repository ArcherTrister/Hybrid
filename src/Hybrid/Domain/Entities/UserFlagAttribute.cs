// -----------------------------------------------------------------------
//  <copyright file="UseTagAttribute.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-20 0:17</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.Domain.Entities
{
    /// <summary>
    /// 用户标记，用于标示用户属性/字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class UserFlagAttribute : Attribute
    {
        /// <summary>
        /// 当前用户标识
        /// </summary>
        public const string Token = "@CurrentUserId";
    }
}