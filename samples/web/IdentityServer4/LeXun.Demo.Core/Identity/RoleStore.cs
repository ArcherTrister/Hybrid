// -----------------------------------------------------------------------
//  <copyright file="RoleStore.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Entity;
using Hybrid.Identity;

using LeXun.Demo.Identity.Entities;

namespace LeXun.Demo.Identity
{
    /// <summary>
    /// 角色仓储
    /// </summary>
    public class RoleStore : RoleStoreBase<Role, int, RoleClaim, int>
    {
        /// <summary>
        /// 初始化一个<see cref="RoleStoreBase{TRole,TRoleKey,TRoleClaim, TRoleClaimKey}"/>类型的新实例
        /// </summary>
        public RoleStore(IRepository<Role, int> roleRepository, IRepository<RoleClaim, int> roleClaimRepository)
            : base(roleRepository, roleClaimRepository)
        { }
    }
}