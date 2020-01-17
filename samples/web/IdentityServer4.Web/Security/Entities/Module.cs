﻿// -----------------------------------------------------------------------
//  <copyright file="Module.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Zero.Security;

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Hybrid.Web.Security.Entities
{
    /// <summary>
    /// 实体类：模块信息
    /// </summary>
    [Description("模块信息")]
    public class Module : ModuleBase<Guid>
    {
        /// <summary>
        /// 获取或设置 父模块信息
        /// </summary>
        public virtual Module Parent { get; set; }

        /// <summary>
        /// 获取或设置 子模块信息集合
        /// </summary>
        public virtual ICollection<Module> Children { get; set; } = new List<Module>();
    }
}