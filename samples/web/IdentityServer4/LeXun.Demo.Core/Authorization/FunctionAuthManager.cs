// -----------------------------------------------------------------------
//  <copyright file="FunctionAuthorizationManager.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-02-27 0:26</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization;
using Hybrid.Authorization.Dtos;
using Hybrid.Authorization.Functions;

using LeXun.Demo.Authorization.Dtos;
using LeXun.Demo.Authorization.Entities;
using LeXun.Demo.Identity.Entities;

using System;

namespace LeXun.Demo.Authorization
{
    /// <summary>
    /// 功能权限管理器
    /// </summary>
    public class FunctionAuthManager
        : FunctionAuthorizationManagerBase<Function, FunctionInputDto, Module, ModuleInputDto, int, ModuleFunction, ModuleRole, ModuleUser, UserRole,
            Guid, Role, int, User, int>
    {
        /// <summary>
        /// 初始化一个 SecurityManager 类型的新实例
        /// </summary>
        /// <param name="provider">服务提供程序</param>
        public FunctionAuthManager(IServiceProvider provider)
            : base(provider)
        { }
    }
}