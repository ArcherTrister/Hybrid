// -----------------------------------------------------------------------
//  <copyright file="ModuleRole.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Web.Identity.Entities;
using Hybrid.Zero.Authorization.Entities;

using System;
using System.ComponentModel;

namespace Hybrid.Web.Authorization.Entities
{
    /// <summary>
    /// 实体类：模块角色信息
    /// </summary>
    [Description("角色模块信息")]
    public class ModuleRole : ModuleRoleBase<Guid, Guid>
    {
        /// <summary>
        /// 获取或设置 模块信息
        /// </summary>
        public virtual Module Module { get; set; }

        /// <summary>
        /// 获取或设置 角色信息
        /// </summary>
        public virtual Role Role { get; set; }
    }
}