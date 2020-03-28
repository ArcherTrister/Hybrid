// -----------------------------------------------------------------------
//  <copyright file="UserRole.cs" company="cn.lxking">
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
    /// 实体类：用户角色信息
    /// </summary>
    [Description("用户角色信息")]
    public class UserRole : UserRoleBase<Guid, Guid, Guid>
    {
        /// <summary>
        /// 获取或设置 关联用户信息
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 获取或设置 关联角色信息
        /// </summary>
        public virtual Role Role { get; set; }
    }
}