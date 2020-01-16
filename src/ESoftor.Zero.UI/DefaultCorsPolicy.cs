// -----------------------------------------------------------------------
//  <copyright file="DefaultCorsPolicy.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using IdentityServer4.Services;

using System.Threading.Tasks;

namespace ESoftor.Zero.IdentityServer4
{
    // allows arbitrary CORS origins - only for demo purposes. NEVER USE IN PRODUCTION
    public class DefaultCorsPolicy : ICorsPolicyService
    {
        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            return Task.FromResult(true);
        }
    }
}