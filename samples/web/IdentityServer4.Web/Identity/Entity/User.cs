// -----------------------------------------------------------------------
//  <copyright file="User.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Permission.Identity;

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ESoftor.Web.Identity.Entity
{
    /// <summary>
    /// 实体类：用户信息
    /// </summary>
    [Description("用户信息")]
    public class User : UserBase<Guid>
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