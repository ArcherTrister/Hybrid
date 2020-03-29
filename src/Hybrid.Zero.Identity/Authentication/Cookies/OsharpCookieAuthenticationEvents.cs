// -----------------------------------------------------------------------
//  <copyright file="HybridCookieAuthenticationEvents.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-02-16 19:00</last-date>
// -----------------------------------------------------------------------

using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

using Hybrid.Authentication.JwtBearer;


namespace Hybrid.Authentication.Cookies
{
    /// <summary>
    /// 自定义 CookieAuthenticationEvents
    /// </summary>
    public class HybridCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        /// <summary>
        /// Cookie验证通过时，从OnlineUser缓存或数据库查找用户的最新信息附加到有效的 ClaimIdentity 上
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            ClaimsPrincipal user = context.Principal;
            ClaimsIdentity identity = user.Identity as ClaimsIdentity;

            IAccessClaimsProvider accessClaimsProvider = context.HttpContext.RequestServices.GetService<IAccessClaimsProvider>();
            return accessClaimsProvider.RefreshIdentity(identity);
        }
    }
}