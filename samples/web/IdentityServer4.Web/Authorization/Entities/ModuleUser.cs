// -----------------------------------------------------------------------
//  <copyright file="ModuleUser.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-01-06 15:25</last-date>
// -----------------------------------------------------------------------

using Hybrid.Web.Identity.Entities;
using Hybrid.Zero.Authorization.Entities;

using System;
using System.ComponentModel;

namespace Hybrid.Web.Authorization.Entities
{
    /// <summary>
    /// 实体类：模块用户信息
    /// </summary>
    [Description("用户模块信息")]
    public class ModuleUser : ModuleUserBase<Guid, Guid>
    {
        /// <summary>
        /// 获取或设置 模块信息
        /// </summary>
        public virtual Module Module { get; set; }

        /// <summary>
        /// 获取或设置 用户信息
        /// </summary>
        public virtual User User { get; set; }
    }
}