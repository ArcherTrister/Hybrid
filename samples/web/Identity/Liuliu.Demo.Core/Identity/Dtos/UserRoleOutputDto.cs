﻿// -----------------------------------------------------------------------
//  <copyright file="UserRoleOutputDto.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Entity;
using Hybrid.Mapping;

using Liuliu.Demo.Identity.Entities;

using System;

namespace Liuliu.Demo.Identity.Dtos
{
    /// <summary>
    /// 输出DTO：用户角色信息
    /// </summary>
    [MapFrom(typeof(UserRole))]
    public class UserRoleOutputDto : IOutputDto, IDataAuthEnabled
    {
        /// <summary>
        /// 初始化一个<see cref="UserRoleOutputDto"/>类型的新实例
        /// </summary>
        public UserRoleOutputDto(UserRole ur)
        {
            Id = ur.Id;
            UserId = ur.UserId;
            RoleId = ur.RoleId;
            IsLocked = ur.IsLocked;
            CreatedTime = ur.CreatedTime;
        }

        /// <summary>
        /// 获取或设置 编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 获取或设置 角色编号
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置 角色名
        /// </summary>
        public string RoleName { get; set; }

        #region Implementation of IDataAuthEnabled

        /// <summary>
        /// 获取或设置 是否可更新的数据权限状态
        /// </summary>
        public bool Updatable { get; set; }

        /// <summary>
        /// 获取或设置 是否可删除的数据权限状态
        /// </summary>
        public bool Deletable { get; set; }

        #endregion Implementation of IDataAuthEnabled
    }
}