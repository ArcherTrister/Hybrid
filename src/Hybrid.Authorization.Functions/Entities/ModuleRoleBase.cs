﻿// -----------------------------------------------------------------------
//  <copyright file="ModuleRoleBase.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-05-15 13:37</last-date>
// -----------------------------------------------------------------------

using Hybrid.Entity;

using System;
using System.ComponentModel;

namespace Hybrid.Authorization.Entities
{
    /// <summary>
    /// 模块角色信息基类
    /// </summary>
    /// <typeparam name="TModuleKey">模块编号</typeparam>
    /// <typeparam name="TRoleKey">角色编号</typeparam>
    [TableNamePrefix("Auth")]
    public abstract class ModuleRoleBase<TModuleKey, TRoleKey> : EntityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 模块编号
        /// </summary>
        [DisplayName("模块编号")]
        public TModuleKey ModuleId { get; set; }

        /// <summary>
        /// 获取或设置 角色编号
        /// </summary>
        [DisplayName("角色编号")]
        public TRoleKey RoleId { get; set; }
    }
}