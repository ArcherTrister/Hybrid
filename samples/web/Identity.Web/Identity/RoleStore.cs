using ESoftor.Domain.Repositories;
using ESoftor.Entity;
using ESoftor.Permission.Identity;
using ESoftor.Web.Identity.Entity;

using System;

namespace ESoftor.Web.Identity
{
    /// <summary>
    /// 角色仓储
    /// </summary>
    public class RoleStore : RoleStoreBase<Role, Guid, RoleClaim>
    {
        /// <summary>
        /// 初始化一个<see cref="RoleStoreBase{TRole,TRoleKey,TRoleClaim}"/>类型的新实例
        /// </summary>
        public RoleStore(IRepository<Role, Guid> roleRepository, IRepository<RoleClaim, Guid> roleClaimRepository)
            : base(roleRepository, roleClaimRepository)
        { }
    }
}
