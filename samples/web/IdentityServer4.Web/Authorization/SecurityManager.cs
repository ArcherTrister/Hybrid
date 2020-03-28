﻿// -----------------------------------------------------------------------
//  <copyright file="SecurityManager.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-04 1:13</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.EntityInfos;
using Hybrid.Authorization.Functions;
using Hybrid.Domain.Repositories;
using Hybrid.EventBuses;
using Hybrid.Web.Authorization.Dtos;
using Hybrid.Web.Authorization.Entities;
using Hybrid.Web.Identity.Entities;
using Hybrid.Zero.Authorization;
using Hybrid.Zero.Authorization.Dtos;

using System;

namespace Hybrid.Web.Authorization
{
    /// <summary>
    /// 权限安全管理器
    /// </summary>
    public class SecurityManager
        : SecurityManagerBase<Function, FunctionInputDto,
            EntityInfo, EntityInfoInputDto, Module, ModuleInputDto, Guid,
            ModuleFunction, ModuleRole, ModuleUser, EntityRole, EntityRoleInputDto,
            UserRole, Guid, Role, Guid, User, Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="SecurityManager"/>类型的新实例
        /// </summary>
        public SecurityManager(
            IEventBus eventBus,
            IRepository<Function, Guid> functionRepository,
            IRepository<EntityInfo, Guid> entityInfoRepository,
            IRepository<Module, Guid> moduleRepository,
            IRepository<ModuleFunction, Guid> moduleFunctionRepository,
            IRepository<ModuleRole, Guid> moduleRoleRepository,
            IRepository<ModuleUser, Guid> moduleUserRepository,
            IRepository<EntityRole, Guid> entityRoleRepository,
            IRepository<UserRole, Guid> userRoleRepository,
            IRepository<Role, Guid> roleRepository,
            IRepository<User, Guid> userRepository
        )
            : base(eventBus,
                functionRepository,
                entityInfoRepository,
                moduleRepository,
                moduleFunctionRepository,
                moduleRoleRepository,
                moduleUserRepository,
                entityRoleRepository,
                userRoleRepository,
                roleRepository,
                userRepository)
        { }
    }
}