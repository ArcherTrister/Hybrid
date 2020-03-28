// -----------------------------------------------------------------------
//  <copyright file="RoleStore.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Domain.Repositories;
using Hybrid.Web.Identity.Entities;
using Hybrid.Zero.Identity.Entities;

using System;

namespace Hybrid.Web.Identity
{
    /// <summary>
    /// 角色仓储
    /// </summary>
    public class RoleStore : RoleStoreBase<Role, Guid, RoleClaim, Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="RoleStoreBase{TRole,TRoleKey,TRoleClaim}"/>类型的新实例
        /// </summary>
        public RoleStore(IRepository<Role, Guid> roleRepository, IRepository<RoleClaim, Guid> roleClaimRepository)
            : base(roleRepository, roleClaimRepository)
        { }
    }
}