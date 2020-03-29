// -----------------------------------------------------------------------
//  <copyright file="AuthenticationPack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-03-02 21:21</last-date>
// -----------------------------------------------------------------------

using LeXun.Demo.Identity.Entities;
using LeXun.Demo.Identity.Events;

using Microsoft.Extensions.DependencyInjection;

using Hybrid.Authentication;
using Hybrid.Core.Packs;


namespace LeXun.Demo.Identity
{
    /// <summary>
    /// 身份认证模块
    /// </summary>
    [DependsOnPacks(typeof(IdentityPack))]
    public class AuthenticationPack : AuthenticationPackBase<User, int>
    {
        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddEventHandler<Logout_RemoveRefreshTokenEventHandler>();
            
            return base.AddServices(services);
        }
    }
}