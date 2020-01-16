// -----------------------------------------------------------------------
//  <copyright file="UserRoleBase.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Domain.Entities;

using System;
using System.ComponentModel;

namespace Hybrid.Zero.Identity
{
    /// <summary>
    /// 用户角色映射基类
    /// </summary>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    public abstract class UserRoleBase<TUserKey, TRoleKey> : EntityBase<Guid>, ICreatedTime, ILockable, ISoftDelete
        where TUserKey : IEquatable<TUserKey>
        where TRoleKey : IEquatable<TRoleKey>
    {
        /// <summary>
        /// 初始化一个<see cref="UserRoleBase{TUserKey, TRoleKey}"/>类型的新实例
        /// </summary>
        protected UserRoleBase()
        {
            CreatedTime = DateTime.Now;
        }

        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        [DisplayName("用户编号")]
        public TUserKey UserId { get; set; }

        /// <summary>
        /// 获取或设置 角色编号
        /// </summary>
        [DisplayName("角色编号")]
        public TRoleKey RoleId { get; set; }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定当前信息
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 数据逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}