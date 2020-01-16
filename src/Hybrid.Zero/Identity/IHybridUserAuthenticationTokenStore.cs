// -----------------------------------------------------------------------
//  <copyright file="IHybridUserAuthenticationTokenStore.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hybrid.Zero.Identity
{
    /// <summary>
    /// 自定义UserAuthenticationTokenStore
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TUserKey"></typeparam>
    public interface IHybridUserAuthenticationTokenStore<TUser, TUserKey> : IUserAuthenticationTokenStore<TUser>
        where TUser : UserBase<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 获取某个用户的所有指定登录提供者的权限标识
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="loginProvider">登录提供者</param>
        /// <param name="cancellationToken">任务取消标识</param>
        /// <returns>权限标识集合</returns>
        Task<string[]> GetTokensAsync(TUser user, string loginProvider, CancellationToken cancellationToken);
    }
}