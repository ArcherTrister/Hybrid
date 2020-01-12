// -----------------------------------------------------------------------
//  <copyright file="UserManagerExtensions.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Exceptions;
using ESoftor.Extensions;
using ESoftor.Identity.JwtBearer;
using ESoftor.Json;

using Microsoft.AspNetCore.Identity;

using System.Threading.Tasks;

namespace ESoftor.Permission.Identity.Extensions
{
    /// <summary>
    /// <see cref="UserManager{TUser}"/>扩展
    /// </summary>
    public static class UserManagerExtensions
    {
        /// <summary>
        /// 获取RefreshToken
        /// </summary>
        public static async Task<RefreshToken> GetRefreshToken<TUser>(this UserManager<TUser> userManager, string userId, string clientId)
            where TUser : class
        {
            TUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ESoftorException($"编号为“{userId}”的用户信息不存在");
            }
            return await userManager.GetRefreshToken(user, clientId);
        }

        /// <summary>
        /// 获取RefreshToken
        /// </summary>
        public static async Task<RefreshToken> GetRefreshToken<TUser>(this UserManager<TUser> userManager, TUser user, string clientId)
            where TUser : class
        {
            const string loginProvider = "JwtBearer";
            string tokenName = $"RefreshToken_{clientId}";
            string json = await userManager.GetAuthenticationTokenAsync(user, loginProvider, tokenName);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            return json.FromJsonString<RefreshToken>();
        }

        /// <summary>
        /// 设置RefreshToken
        /// </summary>
        public static async Task<IdentityResult> SetRefreshToken<TUser>(this UserManager<TUser> userManager, string userId, RefreshToken token)
            where TUser : class
        {
            TUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new IdentityResult().Failed($"编号为“{userId}”的用户信息不存在");
            }
            return await userManager.SetRefreshToken(user, token);
        }

        /// <summary>
        /// 设置RefreshToken
        /// </summary>
        public static Task<IdentityResult> SetRefreshToken<TUser>(this UserManager<TUser> userManager, TUser user, RefreshToken token)
            where TUser : class
        {
            const string loginProvider = "JwtBearer";
            string tokenName = $"RefreshToken_{token.ClientId}";
            string json = token.ToJsonString();
            return userManager.SetAuthenticationTokenAsync(user, loginProvider, tokenName, json);
        }

        /// <summary>
        /// 移除RefreshToken
        /// </summary>
        public static async Task<IdentityResult> RemoveRefreshToken<TUser>(this UserManager<TUser> userManager, string userId, string clientId)
            where TUser : class
        {
            TUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new IdentityResult().Failed($"编号为“{userId}”的用户信息不存在");
            }
            return await RemoveRefreshToken(userManager, user, clientId);
        }

        /// <summary>
        /// 移除RefreshToken
        /// </summary>
        public static Task<IdentityResult> RemoveRefreshToken<TUser>(this UserManager<TUser> userManager, TUser user, string clientId)
            where TUser : class
        {
            const string loginProvider = "JwtBearer";
            string tokenName = $"RefreshToken_{clientId}";
            return userManager.RemoveAuthenticationTokenAsync(user, loginProvider, tokenName);
        }
    }
}