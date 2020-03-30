﻿// -----------------------------------------------------------------------
//  <copyright file="IAccessClaimsProvider.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-06-15 12:11</last-date>
// -----------------------------------------------------------------------

using System.Security.Claims;
using System.Threading.Tasks;

namespace Hybrid.Authentication.JwtBearer
{
    /// <summary>
    /// 定义AccessToken的用户Claims提供器
    /// </summary>
    public interface IAccessClaimsProvider
    {
        /// <summary>
        /// 创建用户标识
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Claim[]> CreateClaims(string userId);

        /// <summary>
        /// 请求的Token验证成功后使用OnlineUser信息刷新Identity，将在线用户信息赋予Identity
        /// </summary>
        /// <param name="identity">待刷新的Identity</param>
        /// <returns>刷新后的Identity</returns>
        Task<ClaimsIdentity> RefreshIdentity(ClaimsIdentity identity);
    }
}