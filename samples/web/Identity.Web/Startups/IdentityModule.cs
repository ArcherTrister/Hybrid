using ESoftor.Core.Options;
using ESoftor.Data;
using ESoftor.Exceptions;
using ESoftor.Permission.Identity;
using ESoftor.Web.Identity;
using ESoftor.Web.Identity.Entity;
using IdentityServer4;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ESoftor.Web.Startups
{
    /// <summary>
    /// 身份认证模块，此模块必须在MVC模块之前启动
    /// </summary>
    [Description("身份认证模块")]
    public class IdentityModule : IdentityModuleBase<UserStore, RoleStore, User, Role, Guid, Guid>
    {
        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 0;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddModule(IServiceCollection services)
        {
            services.AddScoped<IIdentityContract, IdentityService>();

            return base.AddModule(services);
        }

        /// <summary>
        /// 重写以实现<see cref="IdentityOptions"/>的配置
        /// </summary>
        /// <returns></returns>
        protected override Action<IdentityOptions> IdentityOptionsAction()
        {
            return options =>
            {
                //登录
                options.SignIn.RequireConfirmedEmail = false;
                //密码
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                //用户
                options.User.RequireUniqueEmail = false;
                //锁定
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            };
        }

        /// <summary>
        /// 重写以实现<see cref="IdentityServerOptions"/>的配置
        /// </summary>
        /// <returns></returns>
        protected override Action<IdentityServerOptions> IdentityServerOptionsAction()
        {
            return options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseSuccessEvents = true;
            };
        }

        /// <summary>
        /// 重写以实现<see cref="CookieAuthenticationOptions"/>的配置
        /// </summary>
        /// <returns></returns>
        protected override Action<CookieAuthenticationOptions> CookieOptionsAction()
        {
            return options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "esoftor.identity";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
                options.LoginPath = "/#/identity/login";
            };
        }

        /// <summary>
        /// 添加Authentication服务
        /// </summary>
        /// <param name="services">服务集合</param>
        protected override void AddAuthentication(IServiceCollection services)
        {
            IConfiguration configuration = services.GetConfiguration();

            // JwtBearer
            AuthenticationBuilder authenticationBuilder = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            // 1.如果在本项目中使用webapi则添加，并且在UseModule中不能使用app.UseAuthentication
            // 2.在webapi上添加[Authorize(AuthenticationSchemes = IdentityServerConstants.LocalApi.AuthenticationScheme)]标记
            // 3.在本框架中使用ESoftorConstants.LocalApi.AuthenticationScheme
            authenticationBuilder.AddLocalApi(ESoftorConstants.LocalApi.AuthenticationScheme, options => { options.ExpectedScope = IdentityServerConstants.LocalApi.ScopeName; });

            // OAuth2
            IConfigurationSection section = configuration.GetSection("ESoftor:OAuth2");
            IDictionary<string, OAuth2Options> dict = section.Get<Dictionary<string, OAuth2Options>>();
            if (dict == null)
            {
                return;
            }
            foreach (KeyValuePair<string, OAuth2Options> pair in dict)
            {
                OAuth2Options value = pair.Value;
                if (!value.Enabled)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(value.ClientId))
                {
                    throw new ESoftorException($"配置文件中ESoftor:OAuth2配置的{pair.Key}节点的ClientId不能为空");
                }
                if (string.IsNullOrEmpty(value.ClientSecret))
                {
                    throw new ESoftorException($"配置文件中ESoftor:OAuth2配置的{pair.Key}节点的ClientSecret不能为空");
                }
                //https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers
                switch (pair.Key)
                {
                    case "QQ":
                        authenticationBuilder.AddQQ(opts =>
                        {
                            opts.ClientId = value.ClientId;
                            opts.ClientSecret = value.ClientSecret;
                        });
                        break;
                        //case "Microsoft":
                        //    authenticationBuilder.AddMicrosoftAccount(opts =>
                        //    {
                        //        opts.ClientId = value.ClientId;
                        //        opts.ClientSecret = value.ClientSecret;
                        //    });
                        //    break;
                        //case "GitHub":
                        //    authenticationBuilder.AddGitHub(opts =>
                        //    {
                        //        opts.ClientId = value.ClientId;
                        //        opts.ClientSecret = value.ClientSecret;
                        //    });
                        //    break;
                }
            }
        }

        /// <summary>
        /// 重写以实现 AddIdentity 之后的构建逻辑
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override IdentityBuilder AddIdentityBuild(IdentityBuilder builder)
        {
            //如需要昵称唯一，启用下面这个验证码
            //builder.AddUserValidator<UserNickNameValidator<User, Guid>>();
            //builder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, Role>>();
            //builder.AddDefaultUI();
            return builder.AddDefaultTokenProviders();
        }

        /// <summary>
        /// 重写以实现 AddIdentityServer 之后的构建逻辑
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override IIdentityServerBuilder AddIdentityServerBuild(IIdentityServerBuilder builder, IServiceCollection services)
        {
            return builder.AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServer4Config.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServer4Config.GetApis())
                .AddInMemoryClients(IdentityServer4Config.GetClients());

            ////持久化
            ////IConfiguration configuration = services.GetConfiguration();
            ////Add-Migration InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
            ////Add-Migration InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb
            ////IConfigurationSection section = configuration.GetSection("OSharp:Idxxxxxx");
            //string entryAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            //return builder
            //        // this adds the config data from DB (clients, resources)
            //        .AddConfigurationStore(options =>
            //        {
            //            options.ConfigureDbContext = build =>
            //                build.UseSqlServer("",
            //                    sql => sql.MigrationsAssembly(entryAssemblyName));
            //        })
            //        // this adds the operational data from DB (codes, tokens, consents)
            //        .AddOperationalStore(options =>
            //        {
            //            options.ConfigureDbContext = build =>
            //                build.UseSqlServer("",
            //                    sql => sql.MigrationsAssembly(entryAssemblyName));

            //            // this enables automatic token cleanup. this is optional.
            //            options.EnableTokenCleanup = true;
            //            options.TokenCleanupInterval = 30; // interval in seconds
            //        });
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        public override void UseModule(IApplicationBuilder app)
        {
            app.UseIdentityServer();

            //app.UseAuthentication();

            IsEnabled = true;
        }
    }
}
