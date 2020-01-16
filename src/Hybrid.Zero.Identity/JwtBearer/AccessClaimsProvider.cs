// -----------------------------------------------------------------------
//  <copyright file="AccessClaimsProvider.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;
using Hybrid.Exceptions;
using Hybrid.Security.Claims;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hybrid.Zero.Identity
{
    /// <summary>
    /// AccessToken的用户Claims提供器
    /// </summary>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    public class AccessClaimsProvider<TUser, TUserKey> : IAccessClaimsProvider
        where TUser : UserBase<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        private readonly IServiceProvider _provider;

        /// <summary>
        /// 初始化一个<see cref="AccessClaimsProvider{TUser, TUserKey}"/>类型的新实例
        /// </summary>
        public AccessClaimsProvider(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// 创建用户标识
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Claim[]> CreateClaims(string userId)
        {
            Check.NotNullOrEmpty(userId, nameof(userId));

            UserManager<TUser> userManager = _provider.GetService<UserManager<TUser>>();
            TUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ESoftorException($"编号为“{userId}”的用户信息不存在。");
            }

            Claim[] claims =
            {
                new Claim(HybridClaimTypes.UserId, user.Id.ToString()),
                new Claim(HybridClaimTypes.UserName, user.UserName)
            };
            return claims;
        }
    }
}