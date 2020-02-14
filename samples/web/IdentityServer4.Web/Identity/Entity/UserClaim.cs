// -----------------------------------------------------------------------
//  <copyright file="UserClaim.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Zero.Identity;

using System;
using System.ComponentModel;

namespace Hybrid.Web.Identity.Entity
{
    /// <summary>
    /// 实体类：用户声明信息
    /// </summary>
    [Description("用户声明信息")]
    public class UserClaim : UserClaimBase<Guid, Guid>
    {
        /// <summary>
        /// 获取或设置 所属用户
        /// </summary>
        public virtual User User { get; set; }
    }
}