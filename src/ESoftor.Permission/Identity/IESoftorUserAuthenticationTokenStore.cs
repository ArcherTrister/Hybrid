// -----------------------------------------------------------------------
//  <copyright file="IESoftorUserAuthenticationTokenStore.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Identity;

using System.Threading;
using System.Threading.Tasks;

namespace ESoftor.Permission.Identity
{
    /// <summary>
    /// 自定义UserAuthenticationTokenStore
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface IESoftorUserAuthenticationTokenStore<TUser> : IUserAuthenticationTokenStore<TUser>
        where TUser : class
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