// -----------------------------------------------------------------------
//  <copyright file="UserStore.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Domain.Repositories;
using Hybrid.Web.Identity.Entity;
using Hybrid.Zero.Identity;

using Microsoft.AspNetCore.Identity;

using System;

namespace Hybrid.Web.Identity
{
    public class UserStore : UserStoreBase<User, Role, Guid, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
    {
        public UserStore(IRepository<User, Guid> userRepository, IRepository<UserLogin, Guid> userLoginRepository, IRepository<UserClaim, Guid> userClaimRepository, IRepository<UserToken, Guid> userTokenRepository, IRepository<Role, Guid> roleRepository, IRepository<UserRole, Guid> userRoleRepository, IdentityErrorDescriber describer = null) : base(userRepository, userLoginRepository, userClaimRepository, userTokenRepository, roleRepository, userRoleRepository, describer ?? new IdentityErrorDescriber())
        {
        }
    }
}