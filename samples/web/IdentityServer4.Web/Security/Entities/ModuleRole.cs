﻿// -----------------------------------------------------------------------
//  <copyright file="ModuleRole.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using ESoftor.Permission.Security;
using ESoftor.Web.Identity.Entity;

using System;
using System.ComponentModel;

namespace ESoftor.Web.Security.Entities
{
    /// <summary>
    /// 实体类：模块角色信息
    /// </summary>
    [Description("角色模块信息")]
    public class ModuleRole : ModuleRoleBase<Guid, Guid>
    {
        /// <summary>
        /// 获取或设置 模块信息
        /// </summary>
        public virtual Module Module { get; set; }

        /// <summary>
        /// 获取或设置 角色信息
        /// </summary>
        public virtual Role Role { get; set; }
    }
}