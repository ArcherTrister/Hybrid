// -----------------------------------------------------------------------
//  <copyright file="ModuleHandler.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using System;

using Liuliu.Demo.Authorization.Dtos;
using Liuliu.Demo.Authorization.Entities;

using Hybrid.Authorization;


namespace Liuliu.Demo.Authorization
{
    /// <summary>
    /// 模块信息处理器
    /// </summary>
    public class ModuleHandler : ModuleHandlerBase<Module, ModuleInputDto, int, ModuleFunction>
    {
        /// <summary>
        /// 初始化一个<see cref="ModuleHandlerBase{TModule, TModuleInputDto, TModuleKey, TModuleFunction}"/>类型的新实例
        /// </summary>
        public ModuleHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        { }
    }
}