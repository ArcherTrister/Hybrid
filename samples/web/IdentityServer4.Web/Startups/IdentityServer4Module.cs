// -----------------------------------------------------------------------
//  <copyright file="IdentityServer4Module.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Options;
using ESoftor.Data;
using ESoftor.Exceptions;
using ESoftor.Zero.IdentityServer4;
using ESoftor.Web.Identity;
using ESoftor.Web.Identity.Entity;

using IdentityServer4;
using IdentityServer4.Configuration;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;

namespace ESoftor.Web.Startups
{
    /// <summary>
    /// 身份认证模块，此模块必须在MVC模块之前启动
    /// </summary>
    [Description("身份认证模块")]
    public class IdentityServer4Module : IdentityServer4ModuleBase<UserStore, RoleStore, User, Role, Guid, Guid>
    {
        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 1;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddScoped<IIdentityContract, IdentityService>();

            return base.AddServices(services);
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
                //Authentication
                ////设置用于交互用户的主机所混杂的cookie创作方案。如果未设置，则该方案将从主机的默认身份验证方案中推断。当主机中使用AddPolicyScheme作为默认方案时，通常使用此设置。
                //options.Authentication.CookieAuthenticationScheme = IdentityServerConstants.DefaultCheckSessionCookieName;
                //用于检查会话终结点的cookie的名称。默认为idsrv.session
                //options.Authentication.CheckSessionCookieName = "";
                //身份验证cookie生存期(只有在使用IdghtyServer提供的cookie处理程序时才有效)。
                options.Authentication.CookieLifetime = TimeSpan.FromMinutes(720);
                //指定Cookie是否应该是滑动的(只有在使用IdghtyServer提供的Cookie处理程序时才有效)。
                options.Authentication.CookieSlidingExpiration = true;
                //指示用户是否必须通过身份验证才能接受结束会话终结点的参数。默认为false。
                //options.Authentication.RequireAuthenticatedUserForSignOutMessage = false;
                //如果设置，将需要在结束会话回调端点上发出帧-src csp报头，该端点将iframes呈现给客户端以进行前端通道签名通知。默认为true。
                //options.Authentication.RequireCspFrameSrcForSignout = true;

                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseSuccessEvents = true;

                options.UserInteraction = new UserInteractionOptions
                {
                    LoginUrl = "/Account/Login",//【必备】登录地址
                    LogoutUrl = "/Account/Logout",//【必备】退出地址
                    ConsentUrl = "/Consent/Index",//【必备】允许授权同意页面地址
                    ErrorUrl = "/Home/Error", //【必备】错误页面地址
                    LoginReturnUrlParameter = "ReturnUrl",//【必备】设置传递给登录页面的返回URL参数的名称。默认为returnUrl
                    LogoutIdParameter = "logoutId", //【必备】设置传递给注销页面的注销消息ID参数的名称。缺省为logoutId
                    ConsentReturnUrlParameter = "ReturnUrl", //【必备】设置传递给同意页面的返回URL参数的名称。默认为returnUrl
                    ErrorIdParameter = "errorId", //【必备】设置传递给错误页面的错误消息ID参数的名称。缺省为errorId
                    CustomRedirectReturnUrlParameter = "ReturnUrl", //【必备】设置从授权端点传递给自定义重定向的返回URL参数的名称。默认为returnUrl
                    CookieMessageThreshold = 5 //【必备】由于浏览器对Cookie的大小有限制，设置Cookies数量的限制，有效的保证了浏览器打开多个选项卡，一旦超出了Cookies限制就会清除以前的Cookies值
                };
            };
        }

        /// <summary>
        /// 添加Authentication服务
        /// </summary>
        /// <param name="services">服务集合</param>
        protected override void AddAuthentication(IServiceCollection services)
        {
            IConfiguration configuration = services.GetConfiguration();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            AuthenticationBuilder authenticationBuilder = services.AddAuthentication();
            // 1.如果在本项目中使用webapi则添加，并且在UseModule中不能使用app.UseAuthentication
            // 2.在webapi上添加[Authorize(AuthenticationSchemes = IdentityServerConstants.LocalApi.AuthenticationScheme)]标记
            // 3.在本框架中使用ESoftorConstants.LocalApi.AuthenticationScheme
            authenticationBuilder.AddLocalApi(ESoftorConstants.LocalApi.AuthenticationScheme, 
                options => 
                {
                    options.ExpectedScope = ESoftorConstants.LocalApi.ScopeName;
                    options.SaveToken = true;
                });

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
        /// 重写以实现 AddIdentityServer 之后的构建逻辑
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override IIdentityServerBuilder AddIdentityServerBuild(IIdentityServerBuilder builder, IServiceCollection services)
        {
            return builder.AddDeveloperSigningCredential()
                .AddHybridDefaultUI<User, Guid>()
                .AddInMemoryIdentityResources(IdentityServer4Config.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServer4Config.GetApis())
                .AddInMemoryClients(IdentityServer4Config.GetClients());

            ////持久化
            ////IConfiguration configuration = services.GetConfiguration();
            ////Add-Migration InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
            ////Add-Migration InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb
            ////IConfigurationSection section = configuration.GetSection("ESoftor:Idxxxxxx");
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
    }
}