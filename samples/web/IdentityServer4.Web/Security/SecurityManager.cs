// -----------------------------------------------------------------------
//  <copyright file="SecurityManager.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-04 1:13</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.EntityInfos;
using ESoftor.Core.Functions;
using ESoftor.Domain.Repositories;
using ESoftor.EventBuses;
using ESoftor.Zero.Security;
using ESoftor.Zero.Security.Dtos;
using ESoftor.Web.Identity.Entity;
using ESoftor.Web.Security.Dtos;
using ESoftor.Web.Security.Entities;

using System;

namespace ESoftor.Web.Security
{
    /// <summary>
    /// 权限安全管理器
    /// </summary>
    public class SecurityManager
        : SecurityManagerBase<Function, FunctionInputDto, EntityInfo, EntityInfoInputDto,
            Module, ModuleInputDto, Guid, ModuleFunction, ModuleRole, ModuleUser, EntityRole, EntityRoleInputDto, UserRole, Role, Guid, User, Guid>
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