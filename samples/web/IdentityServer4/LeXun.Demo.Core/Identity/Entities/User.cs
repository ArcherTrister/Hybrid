// -----------------------------------------------------------------------
//  <copyright file="User.cs" company="Hybrid开源团队">
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
    /// 实体类：用户信息
    /// </summary>
    [Description("用户信息")]
    public class User : UserBase<int>
    {
        /// <summary>
        /// 获取或设置 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 用户详细信息
        /// </summary>
        public virtual UserDetail UserDetail { get; set; }

        /// <summary>
        /// 获取或设置 分配的用户角色信息集合
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        /// <summary>
        /// 获取或设置 用户的声明信息集合
        /// </summary>
        public virtual ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();

        /// <summary>
        /// 获取或设置 用户的第三方登录信息集合
        /// </summary>
        public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();

        /// <summary>
        /// 获取或设置 用户令牌信息集合
        /// </summary>
        public virtual ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();
    }
}