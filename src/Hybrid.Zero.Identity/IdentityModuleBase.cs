﻿// -----------------------------------------------------------------------
//  <copyright file="IdentityModuleBase.cs" company="cn.lxking">
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
using Hybrid.Security.Claims;
using Hybrid.Zero.Identity.Entities;
using Hybrid.Zero.Identity.RealTime;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;

namespace Hybrid.Zero.Identity
{
    /// <summary>
    /// 身份论证模块基类
    /// </summary>
    [DependsOnModules(typeof(EventBusModule), typeof(AspNetCoreModule))]
    public abstract class IdentityModuleBase<TUserStore, TRoleStore, TUser, TUserKey, TUserClaim, TUserClaimKey, TRole, TRoleKey> : AspHybridModule
        where TUserStore : class, IUserStore<TUser>
        where TRoleStore : class, IRoleStore<TRole>
        where TUser : UserBase<TUserKey>
        where TRole : RoleBase<TRoleKey>
        where TUserKey : IEquatable<TUserKey>
        where TUserClaim : UserClaimBase<TUserClaimKey, TUserKey>
        where TUserClaimKey : IEquatable<TUserClaimKey>
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
            services.AddScoped<IUserStore<TUser>, TUserStore>();
            services.AddScoped<IRoleStore<TRole>, TRoleStore>();

            //在线用户缓存
            services.TryAddScoped<IOnlineUserProvider, OnlineUserProvider<TUser, TUserKey, TUserClaim, TUserClaimKey, TRole, TRoleKey>>();

            // 替换 IPrincipal ，设置用户主键类型，用以在Repository进行审计时注入正确用户主键类型
            services.Replace(new ServiceDescriptor(typeof(IPrincipal),
                provider =>
                {
                    IHttpContextAccessor accessor = provider.GetService<IHttpContextAccessor>();
                    ClaimsPrincipal principal = accessor?.HttpContext?.User;
                    if (principal != null && principal.Identity is ClaimsIdentity identity)
                    {
                        PropertyInfo property = typeof(TUser).GetProperty("Id");
                        if (property != null)
                        {
                            identity.AddClaim(new Claim(HybridClaimTypes.UserIdTypeName, property.PropertyType.FullName));
                        }
                    }

                    return principal;
                },
                ServiceLifetime.Transient));

            Action<IdentityOptions> identityOptionsAction = IdentityOptionsAction();
            IdentityBuilder builder = services.AddIdentity<TUser, TRole>(identityOptionsAction);

            services.Replace(new ServiceDescriptor(typeof(IdentityErrorDescriber), typeof(IdentityErrorDescriberZhHans), ServiceLifetime.Scoped));

            OnIdentityBuild(builder);

            Action<CookieAuthenticationOptions> cookieOptionsAction = CookieOptionsAction();
            if (cookieOptionsAction != null)
            {
                services.ConfigureApplicationCookie(cookieOptionsAction);
            }

            AddAuthentication(services);

            return services;
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
                options.LoginPath = "/#/identity/login";
            };
        }

        /// <summary>
        /// 添加Authentication服务
        /// </summary>
        /// <param name="services">服务集合</param>
        protected virtual void AddAuthentication(IServiceCollection services)
        { }

        /// <summary>
        /// 重写以实现 AddIdentity 之后的构建逻辑
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected virtual IdentityBuilder OnIdentityBuild(IdentityBuilder builder)
        {
            return builder.AddDefaultTokenProviders();
        }
    }
}