// -----------------------------------------------------------------------
//  <copyright file="Module.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.Entities;

using System.Collections.Generic;
using System.ComponentModel;

namespace LeXun.Demo.Authorization.Entities
{
    /// <summary>
    /// 实体类：模块信息
    /// </summary>
    [Description("模块信息")]
    public class Module : ModuleBase<int>
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