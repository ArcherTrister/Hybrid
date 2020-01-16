// -----------------------------------------------------------------------
//  <copyright file="ModuleFunction.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Functions;
using Hybrid.Zero.Security;

using System;
using System.ComponentModel;

namespace Hybrid.Web.Security.Entities
{
    /// <summary>
    /// 实体类：模块功能信息
    /// </summary>
    [Description("模块功能信息")]
    public class ModuleFunction : ModuleFunctionBase<Guid>
    {
        /// <summary>
        /// 获取或设置 模块信息
        /// </summary>
        public virtual Module Module { get; set; }

        /// <summary>
        /// 获取或设置 功能信息
        /// </summary>
        public virtual Function Function { get; set; }
    }
}