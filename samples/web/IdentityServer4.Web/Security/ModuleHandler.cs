// -----------------------------------------------------------------------
//  <copyright file="ModuleHandler.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Web.Security.Dtos;
using Hybrid.Web.Security.Entities;
using Hybrid.Zero.Security;

using System;

namespace Hybrid.Web.Security
{
    /// <summary>
    /// 模块信息处理器
    /// </summary>
    public class ModuleHandler : ModuleHandlerBase<Module, ModuleInputDto, Guid, ModuleFunction>
    {
        /// <summary>
        /// 初始化一个<see cref="ModuleHandlerBase{Tmodule, TmoduleInputDto, TmoduleKey, TmoduleFunction}"/>类型的新实例
        /// </summary>
        public ModuleHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        { }
    }
}