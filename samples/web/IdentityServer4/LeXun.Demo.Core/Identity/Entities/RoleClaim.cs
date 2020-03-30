// -----------------------------------------------------------------------
//  <copyright file="RoleClaim.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Identity.Entities;

using System.ComponentModel;

namespace LeXun.Demo.Identity.Entities
{
    /// <summary>
    /// 实体类：角色声明信息
    /// </summary>
    [Description("角色声明信息")]
    public class RoleClaim : RoleClaimBase<int, int>
    {
        /// <summary>
        /// 获取或设置 所属角色信息
        /// </summary>
        public virtual Role Role { get; set; }
    }
}