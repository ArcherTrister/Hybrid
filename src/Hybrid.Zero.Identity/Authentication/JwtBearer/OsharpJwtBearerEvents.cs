﻿// -----------------------------------------------------------------------
//  <copyright file="HybridJwtBearerEvents.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-02-15 0:15</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using System.Security.Claims;
using System.Threading.Tasks;

namespace Hybrid.Authentication.JwtBearer
{
    /// <summary>
    /// 自定义JwtBearerEvents
    /// </summary>
    public class HybridJwtBearerEvents : JwtBearerEvents
    {
        /// <summary>
        /// Token验证通过时，从OnlineUser缓存或数据库查找用户的最新信息附加到有效的 ClaimPrincipal 上
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenValidated(TokenValidatedContext context)
        {
            ClaimsPrincipal user = context.Principal;
            ClaimsIdentity identity = user.Identity as ClaimsIdentity;

            IAccessClaimsProvider accessClaimsProvider = context.HttpContext.RequestServices.GetService<IAccessClaimsProvider>();
            return accessClaimsProvider.RefreshIdentity(identity);
        }

        /// <summary>
        /// 在接收消息时触发，这里定义接收SignalR的token的逻辑
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task MessageReceived(MessageReceivedContext context)
        {
            string token = context.Request.Query["access_token"];
            string path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(token) && path.Contains("hub"))
            {
                context.Token = token;
            }

            return Task.CompletedTask;
        }

        public override Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }

            return Task.CompletedTask;
        }
    }
}