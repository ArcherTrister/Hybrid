// -----------------------------------------------------------------------
//  <copyright file="IdentityServer4PackBase.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore;
using Hybrid.Core.Configuration;
using Hybrid.Core.Packs;
using Hybrid.EventBuses;
using Hybrid.Identity;
using Hybrid.Identity.Entities;
using Hybrid.Localization;
using Hybrid.Localization.Dictionaries;
using Hybrid.Localization.Dictionaries.Json;
using Hybrid.Reflection;
using Hybrid.Zero.IdentityServer4.Services;
using Hybrid.Zero.IdentityServer4.Services.Impl;
using Hybrid.Zero.IdentityServer4.Stores;

using IdentityServer4.Configuration;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;

namespace Hybrid.Zero.IdentityServer4
{
    /// <summary>
    /// 身份论证模块基类
    /// </summary>
    [DependsOnPacks(typeof(EventBusPack), typeof(AspNetCorePack))]
    public abstract class IdentityServer4PackBase<TUserStore, TRoleStore, TUser, TUserKey, TUserClaim, TUserClaimKey, TRole, TRoleKey> : AspHybridPack
        where TUserStore : class, IUserStore<TUser>
        where TRoleStore : class, IRoleStore<TRole>
        where TUser : UserBase<TUserKey>
        where TRole : RoleBase<TRoleKey>
        where TUserKey : IEquatable<TUserKey>
        where TRoleKey : IEquatable<TRoleKey>
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override PackLevel Level => PackLevel.Application;

        public override int Order => 10;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            //在线用户缓存
            services.TryAddScoped<IOnlineUserProvider, OnlineUserProvider<TUser, TUserKey, TUserClaim, TUserClaimKey, TRole, TRoleKey>>();

            Action<IdentityOptions> identityOptionsAction = IdentityOptionsAction();
            IdentityBuilder builder = services.AddIdentity<TUser, TRole>(identityOptionsAction)
                .AddUserStore<TUserStore>()
                .AddRoleStore<TRoleStore>()
                .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<TUser, TRole>>()
                .AddDefaultTokenProviders();

            services.Replace(new ServiceDescriptor(typeof(IdentityErrorDescriber), typeof(IdentityErrorDescriberZhHans), ServiceLifetime.Scoped));

            //IdentityServer4
            Action<IdentityServerOptions> identityServerOptionsAction = IdentityServerOptionsAction();

            var identityBuilder = services.AddIdentityServer(identityServerOptionsAction).AddHybridIdentity<TUser, TUserKey>();

            services.AddSingleton<IPersistedGrantStore, CustomInMemoryPersistedGrantStore>();

            IConfiguration configuration = services.GetConfiguration();
            IdentityServerConfiguration idsOptions = configuration.GetSection("Hybrid:Ids").Get<IdentityServerConfiguration>();

            if (idsOptions.IsLocalApi)
            {
                AddIdentityServerBuild(identityBuilder, services);
            }

            AddAuthentication(services, idsOptions, configuration);

            //webapi
            services.AddTransient<IPersistedGrantAspNetIdentityService, PersistedGrantAspNetIdentityService<TUser, TUserKey>>();

            Action<CookieAuthenticationOptions> cookieOptionsAction = CookieOptionsAction();
            if (cookieOptionsAction != null)
            {
                services.ConfigureApplicationCookie(cookieOptionsAction);
            }

            return services;
        }

        /// <summary>
        /// 重写以实现<see cref="CookieAuthenticationOptions"/>的配置
        /// </summary>
        /// <returns></returns>
        protected virtual Action<CookieAuthenticationOptions> CookieOptionsAction()
        {
            return options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "hybrid.identity";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
                options.LoginPath = "/Account/Login";
            };
        }

        /// <summary>
        /// 重写以实现<see cref="IdentityOptions"/>的配置
        /// </summary>
        /// <returns></returns>
        protected virtual Action<IdentityOptions> IdentityOptionsAction()
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
        /// <example>
        /// return options =>
        /// {
        ///    options.Events.RaiseErrorEvents = true;
        ///    options.Events.RaiseInformationEvents = true;
        ///    options.Events.RaiseFailureEvents = true;
        ///    options.Events.RaiseSuccessEvents = true;
        /// };
        /// </example>
        /// <returns></returns>
        protected Action<IdentityServerOptions> IdentityServerOptionsAction()
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
                    ErrorUrl = "/IdentityServer/Error", //【必备】错误页面地址
                    LoginReturnUrlParameter = "ReturnUrl",//【必备】设置传递给登录页面的返回URL参数的名称。默认为returnUrl
                    LogoutIdParameter = "logoutId", //【必备】设置传递给注销页面的注销消息ID参数的名称。缺省为logoutId
                    ConsentReturnUrlParameter = "ReturnUrl", //【必备】设置传递给同意页面的返回URL参数的名称。默认为returnUrl
                    ErrorIdParameter = "errorId", //【必备】设置传递给错误页面的错误消息ID参数的名称。缺省为errorId
                    CustomRedirectReturnUrlParameter = "ReturnUrl", //【必备】设置从授权端点传递给自定义重定向的返回URL参数的名称。默认为returnUrl
                    CookieMessageThreshold = 5 //【必备】由于浏览器对Cookie的大小有限制，设置Cookies数量的限制，有效的保证了浏览器打开多个选项卡，一旦超出了Cookies限制就会清除以前的Cookies值
                };
            };
        }

        ///// <summary>
        ///// 重写以实现<see cref="CookieAuthenticationOptions"/>的配置
        ///// </summary>
        ///// <returns></returns>
        //protected virtual Action<CookieAuthenticationOptions> CookieOptionsAction()
        //{
        //    return null;
        //}

        /// <summary>
        /// 添加Authentication服务
        /// </summary>
        /// <param name="services">服务集合</param>
        protected virtual void AddAuthentication(IServiceCollection services, IdentityServerConfiguration idsOptions, IConfiguration configuration)
        { }

        /// <summary>
        /// 重写以实现 AddIdentityServer 之后的构建逻辑
        /// </summary>
        /// <param name="builder"></param>
        /// <example>
        /// .AddInMemoryIdentityResources(Config.GetIdentityResources())
        /// .AddInMemoryApiResources(Config.GetApis())
        /// .AddInMemoryClients(Config.GetClients());
        /// </example>
        protected virtual IIdentityServerBuilder AddIdentityServerBuild(IIdentityServerBuilder builder, IServiceCollection services)
        {
            return builder;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        public override void UsePack(IApplicationBuilder app)
        {
            app.UseIdentityServer();

            app.UseAuthorization();

            IServiceProvider provider = app.ApplicationServices;
            var Configuration = provider.GetService<IHybridStartupConfiguration>();

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    LocalizationConsts.IdentityServerSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(CustomUserInfoResponse).GetAssembly(), "Hybrid.Zero.IdentityServer4.Quickstart.Localization.Sources.JsonSource"
            )));
        }
    }
}