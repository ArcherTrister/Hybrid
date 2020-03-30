// -----------------------------------------------------------------------
//  <copyright file="UserRoleInputDtoBase.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-31 19:21</last-date>
// -----------------------------------------------------------------------

using Hybrid.Entity;

namespace Hybrid.Identity.Dtos
{
    /// <summary>
    /// 用户角色输入DTO基类
    /// </summary>
    public abstract class UserRoleInputDtoBase<TKey, TUserKey, TRoleKey> : IInputDto<TKey>
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
        /// 获取或设置 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public TKey Id { get; set; }
    }
}