// -----------------------------------------------------------------------
//  <copyright file="UserTokenBase.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-31 19:16</last-date>
// -----------------------------------------------------------------------

using Hybrid.Entity;

using System;
using System.ComponentModel;

namespace Hybrid.Identity.Entities
{
    /// <summary>
    /// 表示用户的身份验证令牌的基类
    /// </summary>
    /// <typeparam name="TKey">用户令牌编号类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    [TableNamePrefix("Identity")]
    public abstract class UserTokenBase<TKey, TUserKey> : EntityBase<TKey>
        where TKey : IEquatable<TKey>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        [DisplayName("用户编号")]
        public TUserKey UserId { get; set; }

        /// <summary>
        /// 获取或设置 当前用户标识的登录提供者
        /// </summary>
        [DisplayName("登录提供者")]
        public string LoginProvider { get; set; }

        /// <summary>
        /// 获取或设置 令牌名称
        /// </summary>
        [DisplayName("令牌名称")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 令牌值
        /// </summary>
        [DisplayName("令牌值")]
        public string Value { get; set; }
    }
}