// -----------------------------------------------------------------------
//  <copyright file="DataAuthorizationManager.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-02-27 0:31</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.DataAuthorization;
using Hybrid.Authorization.Dtos;
using Hybrid.Authorization.EntityInfos;
using Hybrid.Entity;
using Hybrid.EventBuses;

using LeXun.Demo.Authorization.Dtos;
using LeXun.Demo.Authorization.Entities;
using LeXun.Demo.Identity.Entities;

using System;

namespace LeXun.Demo.Authorization
{
    /// <summary>
    /// 数据权限管理器
    /// </summary>
    //[Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class DataAuthManager : DataAuthorizationManagerBase<EntityInfo, EntityInfoInputDto, EntityRole, EntityRoleInputDto, Role, int>
    {
        /// <summary>
        /// 初始化一个 SecurityManager 类型的新实例
        /// </summary>
        /// <param name="eventBus">事件总线</param>
        /// <param name="entityInfoRepository">实体仓储</param>
        /// <param name="entityRoleRepository">实体角色仓储</param>
        /// <param name="roleRepository">角色仓储</param>
        public DataAuthManager(IEventBus eventBus,
            IRepository<EntityInfo, Guid> entityInfoRepository,
            IRepository<EntityRole, Guid> entityRoleRepository,
            IRepository<Role, int> roleRepository)
            : base(eventBus, entityInfoRepository, entityRoleRepository, roleRepository)
        { }
    }
}