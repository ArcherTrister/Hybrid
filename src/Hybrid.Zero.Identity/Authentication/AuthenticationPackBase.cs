﻿// -----------------------------------------------------------------------
//  <copyright file="AuthenticationPackBase.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-03-02 22:23</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore;
using Hybrid.Authentication.JwtBearer;
using Hybrid.Core.Packs;
using Hybrid.Exceptions;
using Hybrid.Extensions;
using Hybrid.Identity.Entities;
using Hybrid.Identity.JwtBearer;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

using System;
using System.ComponentModel;
using System.Text;

namespace Hybrid.Authentication
{
    /// <summary>
    /// 身份认证模块基类
    /// </summary>
    [Description("身份认证模块")]
    [DependsOnPacks(typeof(AspNetCorePack))]
    public abstract class AuthenticationPackBase<TUser, TUserKey> : AspHybridPack
        where TUser : UserBase<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override PackLevel Level => PackLevel.Application;

        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，同一级别内部再按此顺序启动，
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
            AuthenticationBuilder builder = services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            AddJwtBearer(services, builder);
            AddCookie(services, builder);
            AddOAuth2(services, builder);

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        public override void UsePack(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }

        /// <summary>
        /// 添加JwtBearer支持
        /// </summary>
        protected virtual AuthenticationBuilder AddJwtBearer(IServiceCollection services, AuthenticationBuilder builder)
        {
            services.TryAddScoped<IJwtBearerService, JwtBearerService<TUser, TUserKey>>();
            services.TryAddScoped<IAccessClaimsProvider, AccessClaimsProvider<TUser, TUserKey>>();

            IConfiguration configuration = services.GetConfiguration();
            builder.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                jwt =>
                {
                    string secret = configuration["Hybrid:Jwt:Secret"];
                    if (secret.IsNullOrEmpty())
                    {
                        throw new HybridException("配置文件中Hybrid配置的Jwt节点的Secret不能为空");
                    }

                    jwt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = configuration["Hybrid:Jwt:Issuer"] ?? "hybrid identity",
                        ValidAudience = configuration["Hybrid:Jwt:Audience"] ?? "hybrid client",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                        LifetimeValidator = (nbf, exp, token, param) => exp > DateTime.UtcNow
                    };

                    jwt.Events = new HybridJwtBearerEvents();
                });

            return builder;
        }

        protected virtual AuthenticationBuilder AddCookie(IServiceCollection services, AuthenticationBuilder builder)
        {
            //builder.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
            //    opts =>
            //    {
            //        opts.Events = new HybridCookieAuthenticationEvents();
            //    });
            return builder;
        }

        /// <summary>
        /// 添加OAuth2第三方登录配置
        /// </summary>
        protected virtual AuthenticationBuilder AddOAuth2(IServiceCollection services, AuthenticationBuilder builder)
        {
            return builder;
        }
    }
}