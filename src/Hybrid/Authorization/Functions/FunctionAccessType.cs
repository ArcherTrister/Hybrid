// -----------------------------------------------------------------------
//  <copyright file="FunctionAccessType.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-02-10 20:13</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;

namespace Hybrid.Authorization.Functions
{
    /// <summary>
    /// 功能访问类型
    /// </summary>
    public enum FunctionAccessType
    {
        /// <summary>
        /// 匿名用户可访问
        /// </summary>
        [Description("匿名访问")] Anonymous = 0,

        /// <summary>
        /// 登录用户可访问
        /// </summary>
        [Description("登录访问")] LoggedIn = 1,

        /// <summary>
        /// 指定角色可访问
        /// </summary>
        [Description("角色访问")] RoleLimit = 2
    }
}