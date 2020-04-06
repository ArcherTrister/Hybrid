// -----------------------------------------------------------------------
//  <copyright file="AuthenticationPack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-03-02 21:21</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authentication;
using Hybrid.Core.Options;
using Hybrid.Core.Packs;
using Hybrid.Exceptions;

using Liuliu.Demo.Identity.Entities;
using Liuliu.Demo.Identity.Events;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Collections.Generic;

namespace Liuliu.Demo.Identity
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

        /// <summary>
        /// 添加OAuth2第三方登录配置
        /// </summary>
        protected override AuthenticationBuilder AddOAuth2(IServiceCollection services, AuthenticationBuilder builder)
        {
            IConfiguration configuration = services.GetConfiguration();
            IConfigurationSection section = configuration.GetSection("Hybrid:OAuth2");
            IDictionary<string, OAuth2Options> dict = section.Get<Dictionary<string, OAuth2Options>>();
            if (dict == null)
            {
                return builder;
            }

            foreach (var (name, options) in dict)
            {
                if (!options.Enabled)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(options.ClientId))
                {
                    throw new HybridException($"配置文件中Hybrid:OAuth2配置的{name}节点的ClientId不能为空");
                }

                if (string.IsNullOrEmpty(options.ClientSecret))
                {
                    throw new HybridException($"配置文件中Hybrid:OAuth2配置的{name}节点的ClientSecret不能为空");
                }

                switch (name)
                {
                    case "QQ":
                        builder.AddQQ(opts =>
                        {
                            opts.ClientId = options.ClientId;
                            opts.ClientSecret = options.ClientSecret;
                        });
                        break;

                    case "Microsoft":
                        builder.AddMicrosoftAccount(opts =>
                        {
                            opts.ClientId = options.ClientId;
                            opts.ClientSecret = options.ClientSecret;
                        });
                        break;

                    case "GitHub":
                        builder.AddGitHub(opts =>
                        {
                            opts.ClientId = options.ClientId;
                            opts.ClientSecret = options.ClientSecret;
                        });
                        break;
                }
            }

            return builder;
        }
    }
}