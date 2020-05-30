// -----------------------------------------------------------------------
//  <copyright file="CodeProject.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-01-08 14:12</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;

using Hybrid.Entity;


namespace Hybrid.CodeGeneration.Entities
{
    /// <summary>
    /// 实体类：代码项目信息
    /// </summary>
    [Description("代码项目信息")]
    public class CodeProject : EntityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 公司
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 获取或设置 项目命名空间前缀，通常形如“公司.项目”
        /// </summary>
        public string NamespacePrefix { get; set; }

        /// <summary>
        /// 获取或设置 站点地址
        /// </summary>
        public string SiteUrl { get; set; }

        /// <summary>
        /// 获取或设置 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 获取或设置 Copyright
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// 获取或设置 模块信息集合
        /// </summary>
        public virtual ICollection<CodeModule> Modules { get; set; } = new List<CodeModule>();
    }
}