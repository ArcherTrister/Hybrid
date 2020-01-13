// -----------------------------------------------------------------------
//  <copyright file="UserStore.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Domain.Repositories;
using ESoftor.Zero.Identity;
using ESoftor.Web.Identity.Entity;

using Microsoft.AspNetCore.Identity;

using System;

namespace ESoftor.Web.Identity
{
    public class UserStore : UserStoreBase<User, Role, Guid, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
    {
        public UserStore(IRepository<User, Guid> userRepository, IRepository<UserLogin, Guid> userLoginRepository, IRepository<UserClaim, Guid> userClaimRepository, IRepository<UserToken, Guid> userTokenRepository, IRepository<Role, Guid> roleRepository, IRepository<UserRole, Guid> userRoleRepository, IdentityErrorDescriber describer = null) : base(userRepository, userLoginRepository, userClaimRepository, userTokenRepository, roleRepository, userRoleRepository, describer ?? new IdentityErrorDescriber())
        {
        }
    }
}