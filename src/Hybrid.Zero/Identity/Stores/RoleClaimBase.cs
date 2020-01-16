// -----------------------------------------------------------------------
//  <copyright file="RoleClaimBase.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Domain.Entities;

using System;
using System.ComponentModel;
using System.Security.Claims;

namespace Hybrid.Zero.Identity
{
    /// <summary>
    /// 角色声明信息基类
    /// </summary>
    /// <typeparam name="TRoleKey">角色主键类型</typeparam>
    public abstract class RoleClaimBase<TRoleKey> : EntityBase<Guid>
        where TRoleKey : IEquatable<TRoleKey>
    {
        /// <summary>
        /// 获取或设置 角色编号
        /// </summary>
        [DisplayName("角色编号")]
        public TRoleKey RoleId { get; set; }

        /// <summary>
        /// 获取或设置 声明类型
        /// </summary>
        [DisplayName("声明类型")]
        public string ClaimType { get; set; }

        /// <summary>
        /// 获取或设置 声明值
        /// </summary>
        [DisplayName("声明值")]
        public string ClaimValue { get; set; }

        /// <summary>
        /// 使用类型和值创建一个声明对象
        /// </summary>
        /// <returns></returns>
        public virtual Claim ToClaim()
        {
            return new Claim(ClaimType, ClaimValue);
        }

        /// <summary>
        /// 使用一个声明对象初始化
        /// </summary>
        /// <param name="other">声明对象</param>
        public virtual void InitializeFromClaim(Claim other)
        {
            ClaimType = other?.Type;
            ClaimValue = other?.Value;
        }
    }
}