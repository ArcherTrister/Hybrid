// -----------------------------------------------------------------------
//  <copyright file="SwaggerPackBase.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-04-15 16:26</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore;
using Hybrid.Core.Packs;
using Hybrid.Exceptions;
using Hybrid.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hybrid.Swagger
{
    /// <summary>
    /// Swagger模块基类
    /// </summary>
    [DependsOnPacks(typeof(AspNetCorePack))]
    public abstract class SwaggerPackBase : AspHybridPack
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Application;

        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 2;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            IConfiguration configuration = services.GetConfiguration();

            string url = configuration["Hybrid:Swagger:Url"];
            if (string.IsNullOrEmpty(url))
            {
                throw new HybridException("配置文件中Swagger节点的Url不能为空");
            }

            string title = configuration["Hybrid:Swagger:Title"];
            int version = configuration["Hybrid:Swagger:Version"].CastTo(1);
            //bool identityServerIsEnable = configuration["Hybrid:Ids:Authority"].CastTo(false);
            string identityServerUrl = configuration["Hybrid:Ids:Authority"].CastTo("");

            services.AddMvcCore().AddApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc($"v{version}", new OpenApiInfo() { Title = title, Version = $"{version}" });

                Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(file =>
                {
                    options.IncludeXmlComments(file);
                });
                //权限Token
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "请输入带有Bearer的Token，形如 “Bearer {Token}” ",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityDefinition("IdentityServer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            //授权地址
                            AuthorizationUrl = new Uri($"{identityServerUrl}/connect/authorize"),
                            TokenUrl = new Uri($"{identityServerUrl}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "IdentityServerApi", "IdentityServerApi授权" },
                            }
                        }
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { "readAccess", "writeAccess" }
                    },
                      {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "IdentityServer" }
                          },
                          new[] { "readAccess", "writeAccess" }
                      }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>(); // 添加IdentityServer4认证过滤
            });

            return services;
        }

        /// <summary>
        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        public override void UsePack(IApplicationBuilder app)
        {
            IConfiguration configuration = app.ApplicationServices.GetService<IConfiguration>();

            app.UseSwagger().UseSwaggerUI(options =>
            {
                string url = configuration["Hybrid:Swagger:Url"];
                string title = configuration["Hybrid:Swagger:Title"];
                int version = configuration["Hybrid:Swagger:Version"].CastTo(1);
                options.SwaggerEndpoint(url, $"{title} V{version}");
                bool miniProfilerEnabled = configuration["Hybrid:Swagger:MiniProfiler"].CastTo(false);
                if (miniProfilerEnabled)
                {
                    options.IndexStream = () => GetType().Assembly.GetManifestResourceStream("Hybrid.Swagger.index.html");
                }
                options.OAuthClientId("swaggerClient");//客服端名称
            });
        }
    }
}