// -----------------------------------------------------------------------
//  <copyright file="RoleClaim.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Zero.Identity.Entities;

using System;
using System.ComponentModel;

namespace Hybrid.Web.Identity.Entities
{
    /// <summary>
    /// 实体类：角色声明信息
    /// </summary>
    [Description("角色声明信息")]
    public class RoleClaim : RoleClaimBase<Guid, Guid>
    {
        /// <summary>
        /// 获取或设置 所属角色信息
        /// </summary>
        public virtual Role Role { get; set; }
    }
}