// -----------------------------------------------------------------------
//  <copyright file="IdentityServer4ModuleBase.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore;
using Hybrid.Core.Modules;
using Hybrid.EventBuses;
using Hybrid.RealTime;
using Hybrid.Zero.Identity;

using IdentityServer4.Configuration;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;

namespace Hybrid.Zero.IdentityServer4
{
    /// <summary>
    /// 身份论证模块基类
    /// </summary>
    [DependsOnModules(typeof(EventBusModule), typeof(AspNetCoreModule))]
    public abstract class IdentityServer4ModuleBase<TUserStore, TRoleStore, TUser, TRole, TUserKey, TRoleKey> : AspHybridModule
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
        public override ModuleLevel Level => ModuleLevel.Application;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            //TODO:在线用户缓存
            //services.TryAddScoped<IOnlineUserProvider, OnlineUserProvider<TUser, TUserKey, TRole, TRoleKey>>();

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

            AddIdentityServerBuild(identityBuilder, services);

            //Action<CookieAuthenticationOptions> cookieOptionsAction = CookieOptionsAction();
            //if (cookieOptionsAction != null)
            //{
            //    services.ConfigureApplicationCookie(cookieOptionsAction);
            //}

            AddAuthentication(services);

            return services;
        }

        /// <summary>
        /// 重写以实现<see cref="IdentityOptions"/>的配置
        /// </summary>
        /// <returns></returns>
        protected virtual Action<IdentityOptions> IdentityOptionsAction()
        {
            return null;
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
        protected virtual Action<IdentityServerOptions> IdentityServerOptionsAction()
        {
            return null;
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
        protected virtual void AddAuthentication(IServiceCollection services)
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
        public override void UseModule(IApplicationBuilder app)
        {
            app.UseIdentityServer();

            app.UseAuthorization();

            IsEnabled = true;
        }
    }
}