﻿// -----------------------------------------------------------------------
//  <copyright file="OnlineUserProvider.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Caching;
using ESoftor.Data;
using ESoftor.Domain.Uow;
using ESoftor.Extensions;
using ESoftor.Identity;
using ESoftor.Identity.JwtBearer;
using ESoftor.Zero.Identity.Extensions;
using ESoftor.Threading.Asyncs;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ESoftor.Zero.Identity
{
    /// <summary>
    /// 在线用户信息提供者
    /// </summary>
    public class OnlineUserProvider<TUser, TUserKey, TRole, TRoleKey> : IOnlineUserProvider
        where TUser : UserBase<TUserKey>
        where TUserKey : IEquatable<TUserKey>
        where TRole : RoleBase<TRoleKey>
        where TRoleKey : IEquatable<TRoleKey>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDistributedCache _cache;
        private static readonly AsyncLock _asyncLock = new AsyncLock();

        /// <summary>
        /// 初始化一个<see cref="OnlineUserProvider{TUser, TUserKey, TRole, TRoleKey}"/>类型的新实例
        /// </summary>
        public OnlineUserProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _cache = serviceProvider.GetService<IDistributedCache>();
        }

        /// <summary>
        /// 获取或创建在线用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>在线用户信息</returns>
        public virtual async Task<OnlineUser> GetOrCreate(string userName)
        {
            string key = $"Identity_OnlineUser_{userName}";
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.SetSlidingExpiration(TimeSpan.FromMinutes(30));
            using (await _asyncLock.LockAsync())
            {
                return await _cache.GetAsync<OnlineUser>(key,
                    async () =>
                    {
                        UserManager<TUser> userManager = _serviceProvider.GetService<UserManager<TUser>>();
                        TUser user = await userManager.FindByNameAsync(userName);
                        if (user == null)
                        {
                            return null;
                        }

                        IList<string> roles = await userManager.GetRolesAsync(user);
                        RoleManager<TRole> roleManager = _serviceProvider.GetService<RoleManager<TRole>>();
                        bool isAdmin = roleManager.Roles.ToList().Any(m => roles.Contains(m.Name) && m.IsAdmin);
                        RefreshToken[] refreshTokens = await GetRefreshTokens(user);
                        return new OnlineUser()
                        {
                            Id = user.Id.ToString(),
                            UserName = user.UserName,
                            NickName = user.NickName,
                            Email = user.Email,
                            HeadImg = user.HeadImg,
                            IsAdmin = isAdmin,
                            Roles = roles.ToArray(),
                            RefreshTokens = refreshTokens.ToDictionary(m => m.ClientId, m => m)
                        };
                    },
                    options);
            }
        }

        /// <summary>
        /// 移除在线用户信息
        /// </summary>
        /// <param name="userNames">用户名</param>
        public virtual void Remove(params string[] userNames)
        {
            Check.NotNull(userNames, nameof(userNames));
            foreach (string userName in userNames)
            {
                string key = $"Identity_OnlineUser_{userName}";
                _cache.Remove(key);
            }
        }

        /// <summary>
        /// 获取指定用户所有刷新Token，并清除过期Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<RefreshToken[]> GetRefreshTokens(TUser user)
        {
            IESoftorUserAuthenticationTokenStore<TUser> store =
                _serviceProvider.GetService<IUserStore<TUser>>() as IESoftorUserAuthenticationTokenStore<TUser>;
            if (store == null)
            {
                return new RefreshToken[0];
            }
            const string loginProvider = "JwtBearer";
            string[] jsons = await store.GetTokensAsync(user, loginProvider, CancellationToken.None);
            if (jsons.Length == 0)
            {
                return new RefreshToken[0];
            }

            RefreshToken[] tokens = jsons.Select(m => m.FromJsonString<RefreshToken>()).ToArray();
            RefreshToken[] expiredTokens = tokens.Where(m => m.EndUtcTime < DateTime.UtcNow).ToArray();
            if (expiredTokens.Length <= 0)
            {
                return tokens;
            }

            //删除过期的Token
            using (var scope = _serviceProvider.CreateScope())
            {
                IServiceProvider scopedProvider = scope.ServiceProvider;
                UserManager<TUser> userManager = scopedProvider.GetService<UserManager<TUser>>();
                foreach (RefreshToken expiredToken in expiredTokens)
                {
                    await userManager.RemoveRefreshToken(user, expiredToken.ClientId);
                }

                IUnitOfWork unitOfWork = scopedProvider.GetUnitOfWork<TUser, TUserKey>();
                unitOfWork.Commit();
            }

            return tokens.Except(expiredTokens).ToArray();
        }
    }
}