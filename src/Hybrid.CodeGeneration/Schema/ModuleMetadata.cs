// -----------------------------------------------------------------------
//  <copyright file="ModuleMetadata.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-01-07 0:09</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using Hybrid.Collections;


namespace Hybrid.CodeGeneration.Schema
{
    /// <summary>
    /// 模块信息元数据
    /// </summary>
    public class ModuleMetadata
    {
        /// <summary>
        /// 获取或设置 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 模块显示名称
        /// </summary>
        public string Display { get; set; }

        private ProjectMetadata _project;

        /// <summary>
        /// 获取或设置 所属项目
        /// </summary>
        public ProjectMetadata Project
        {
            get => _project;
            set
            {
                _project = value;
                value.Modules.AddIfNotExist(this);
            }
        }

        /// <summary>
        /// 获取 模块命名空间
        /// </summary>
        public string Namespace => $"{Project?.NamespacePrefix}.{Name}";

        /// <summary>
        /// 获取或设置 实体元数据集合
        /// </summary>
        public ICollection<EntityMetadata> Entities { get; set; } = new List<EntityMetadata>();
    }
}