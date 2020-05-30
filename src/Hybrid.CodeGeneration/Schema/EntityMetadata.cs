// -----------------------------------------------------------------------
//  <copyright file="EntityMetadata.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-06 12:25</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Hybrid.Collections;
using Hybrid.Exceptions;


namespace Hybrid.CodeGeneration.Schema
{
    /// <summary>
    /// 实体元数据
    /// </summary>
    public class EntityMetadata
    {
        private ModuleMetadata _module;

        /// <summary>
        /// 获取或设置 类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 类型显示名称
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// 获取或设置 主键类型全名
        /// </summary>
        public string PrimaryKeyTypeFullName { get; set; }

        /// <summary>
        /// 获取或设置 是否数据权限控制
        /// </summary>
        public bool IsDataAuth { get; set; }

        /// <summary>
        /// 获取或设置 所属模块信息
        /// </summary>
        public ModuleMetadata Module
        {
            get => _module;
            set
            {
                _module = value;
                value.Entities.AddIfNotExist(this);
            }
        }

        /// <summary>
        /// 获取或设置 实体属性元数据集合
        /// </summary>
        public ICollection<PropertyMetadata> Properties { get; set; } = new List<PropertyMetadata>();
    }
}