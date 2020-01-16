// -----------------------------------------------------------------------
//  <copyright file="UserRoleInputDtoBase.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Domain.Entities;

using System;

namespace Hybrid.Zero.Identity
{
    /// <summary>
    /// 用户角色输入DTO基类
    /// </summary>
    public abstract class UserRoleInputDtoBase<TUserKey, TRoleKey> : IInputDto<Guid>
    {
        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public TUserKey UserId { get; set; }

        /// <summary>
        /// 获取或设置 角色编号
        /// </summary>
        public TRoleKey RoleId { get; set; }

        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }
    }
}