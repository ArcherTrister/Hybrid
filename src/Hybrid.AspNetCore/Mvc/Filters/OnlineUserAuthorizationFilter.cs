using Hybrid.Dependency;
using Hybrid.Extensions;
using Hybrid.RealTime;
using Hybrid.Security.Claims;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hybrid.AspNetCore.Mvc.Filters
{
    /// <summary>
    /// 在线用户信息过滤器
    /// </summary>
    public class OnlineUserAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// Called early in the filter pipeline to confirm request is authorized.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext" />.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that on completion indicates the filter has executed.
        /// </returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            ClaimsPrincipal principal = context.HttpContext.User;
            ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
            IServiceProvider provider = context.HttpContext.RequestServices;
            if (identity != null && identity.IsAuthenticated)
            {
                // 由在线缓存获取用户信息赋给Identity
                IOnlineUserProvider onlineUserProvider = provider.GetService<IOnlineUserProvider>();
                OnlineUserBase onlineUser = await onlineUserProvider.GetOrCreate(identity.Name);
                if (onlineUser == null)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(onlineUser.NickName))
                {
                    identity.AddClaim(new Claim(HybridClaimTypes.NickName, onlineUser.NickName));
                }
                if (!string.IsNullOrEmpty(onlineUser.Email))
                {
                    identity.AddClaim(new Claim(HybridClaimTypes.Email, onlineUser.Email));
                }
                if (onlineUser.Roles.Length > 0)
                {
                    identity.AddClaim(new Claim(HybridClaimTypes.Role, onlineUser.Roles.ExpandAndToString()));
                }

                //扩展数据
                foreach (KeyValuePair<string, string> pair in onlineUser.ExtendData)
                {
                    identity.AddClaim(new Claim(pair.Key, pair.Value));
                }
            }

            ScopedDictionary dict = provider.GetService<ScopedDictionary>();
            dict.Identity = identity;
        }
    }
}
