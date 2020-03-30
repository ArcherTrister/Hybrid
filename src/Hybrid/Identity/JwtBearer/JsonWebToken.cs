// -----------------------------------------------------------------------
//  <copyright file="JsonWebToken.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-06-12 15:31</last-date>
// -----------------------------------------------------------------------

using Hybrid.Extensions;
using Hybrid.Timing;

using System;

namespace Hybrid.Identity.JwtBearer
{
    /// <summary>
    /// JwtToken模型
    /// </summary>
    public class JsonWebToken
    {
        /// <summary>
        /// 获取或设置 用于业务身份认证的AccessToken
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 获取或设置 用于刷新AccessToken的RefreshToken
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 获取或设置 RefreshToken有效期，UTC标准
        /// </summary>
        public long RefreshUctExpires { get; set; }

        /// <summary>
        /// 刷新Token是否过期
        /// </summary>
        public bool IsRefreshExpired()
        {
            DateTime now = DateTime.Now;
            long nowTick = now.ToJsGetTime().CastTo<long>(0);
            return RefreshUctExpires > nowTick;
        }
    }
}