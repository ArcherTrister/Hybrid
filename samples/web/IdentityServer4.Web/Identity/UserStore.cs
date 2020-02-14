// -----------------------------------------------------------------------
//  <copyright file="UserStore.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Domain.Repositories;
using Hybrid.EventBuses;
using Hybrid.Web.Identity.Entity;
using Hybrid.Zero.Identity;

using System;

namespace Hybrid.Web.Identity
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public class UserStore : UserStoreBase<User, Guid, UserClaim, Guid, UserLogin, Guid, UserToken, Guid, Role, Guid, UserRole, Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="UserStoreBase{TUser, TUserKey, TUserClaim, TUserClaimKey, TUserLogin, TUserLoginKey, TUserToken, TUserTokenKey, TRole, TRoleKey, TUserRole, TUserRoleKey}"/>类型的新实例
        /// </summary>
        /// <param name="userRepository">用户仓储</param>
        /// <param name="userLoginRepository">用户登录仓储</param>
        /// <param name="userClaimRepository">用户声明仓储</param>
        /// <param name="userTokenRepository">用户令牌仓储</param>
        /// <param name="roleRepository">角色仓储</param>
        /// <param name="userRoleRepository">用户角色仓储</param>
        /// <param name="eventBus">事件总线</param>
        public UserStore(IRepository<User, Guid> userRepository,
            IRepository<UserLogin, Guid> userLoginRepository,
            IRepository<UserClaim, Guid> userClaimRepository,
            IRepository<UserToken, Guid> userTokenRepository,
            IRepository<Role, Guid> roleRepository,
            IRepository<UserRole, Guid> userRoleRepository,
            IEventBus eventBus)
            : base(userRepository, userLoginRepository, userClaimRepository, userTokenRepository, roleRepository, userRoleRepository, eventBus)
        { }
    }
}