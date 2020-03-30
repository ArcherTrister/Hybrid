// -----------------------------------------------------------------------
//  <copyright file="Role.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Identity.Entities;

using System.Collections.Generic;
using System.ComponentModel;

namespace LeXun.Demo.Identity.Entities
{
    /// <summary>
    /// 实体类：角色信息
    /// </summary>
    [Description("角色信息")]
    public class Role : RoleBase<int>
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