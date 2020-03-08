﻿// -----------------------------------------------------------------------
//  <copyright file="ModuleFunctionBase.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Domain.Entities;

using System;
using System.ComponentModel;

namespace Hybrid.Zero.Authorization.Entities
{
    /// <summary>
    /// 模块功能信息基类
    /// </summary>
    public abstract class ModuleFunctionBase<TModuleKey> : EntityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 模块编号
        /// </summary>
        [DisplayName("模块编号")]
        public TModuleKey ModuleId { get; set; }

        /// <summary>
        /// 获取或设置 功能编号
        /// </summary>
        [DisplayName("功能编号")]
        public Guid FunctionId { get; set; }
    }
}