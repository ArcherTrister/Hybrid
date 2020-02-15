// -----------------------------------------------------------------------
//  <copyright file="Role.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Zero.Identity;

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Hybrid.Web.Identity.Entities
{
    /// <summary>
    /// 实体类：角色信息
    /// </summary>
    [Description("角色信息")]
    public class Role : RoleBase<Guid>
    {
        /// <summary>
        /// 获取或设置 分配的用户角色信息集合
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        /// <summary>
        /// 获取或设置 角色声明信息集合
        /// </summary>
        public virtual ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();
    }
}