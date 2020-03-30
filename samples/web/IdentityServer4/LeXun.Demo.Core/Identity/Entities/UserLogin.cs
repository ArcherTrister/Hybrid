// -----------------------------------------------------------------------
//  <copyright file="UserLogin.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Identity.Entities;

using System;
using System.ComponentModel;

namespace LeXun.Demo.Identity.Entities
{
    /// <summary>
    /// 实体类：用户登录及其提供程序
    /// </summary>
    [Description("用户登录及其提供程序")]
    public class UserLogin : UserLoginBase<Guid, int>
    {
        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public virtual User User { get; set; }
    }
}