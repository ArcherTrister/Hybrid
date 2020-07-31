﻿// -----------------------------------------------------------------------
//  <copyright file="UserLogin.cs" company="ESoftor开源团队">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using ESoftor.Permission.Identity;

using System;
using System.ComponentModel;


namespace ESoftor.Web.Identity.Entity
{
    /// <summary>
    /// 实体类：用户登录及其提供程序
    /// </summary>
    [Description("用户登录及其提供程序")]
    public class UserLogin : UserLoginBase<Guid>
    {
        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public virtual User User { get; set; }
    }
}