// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;

using IdentityServer4AspNetIdentity.Data;
using IdentityServer4AspNetIdentity.Models;

using IdentityServerAspNetIdentity;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json.Serialization;

using Swashbuckle.AspNetCore.Swagger;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IdentityServer4AspNetIdentity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(options =>
            {
                //options.Conventions.Add(new DashedRoutingConvention());
                //options.Filters.Add(new OnlineUserAuthorizationFilter()); // 构建在线用户信息
                options.Filters.Add(new FunctionAuthorizationFilter()); // 全局功能权限过滤器
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
                .AddInMemoryIdentityResources(Config.Ids)
                .AddInMemoryApiResources(Config.Apis)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<ApplicationUser>();

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            services.AddAuthentication()
                    .AddLocalApi(options =>
                    {
                        options.ExpectedScope = IdentityServerConstants.LocalApi.ScopeName;
                        options.SaveToken = true;
                    });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "esoftor web api", Version = "v1" });
                Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(file =>
                {
                    options.IncludeXmlComments(file);
                });
                //向生成的Swagger添加一个或多个“securityDefinitions”，用于API的登录校验
                //权限Token
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Bearer 授权 \"Authorization:     Bearer+空格+token\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                options.AddSecurityDefinition("IdentityServer",
                    new OAuth2Scheme
                    {
                        Type = "oauth2",
                        Flow = "implicit",
                        AuthorizationUrl = "http://localhost:5002/connect/authorize",
                        Scopes = new Dictionary<string, string>
                        {
                            { IdentityServerConstants.LocalApi.ScopeName, "IdentityServerApi授权" }
                        }
                    });

                var securityRequirement = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new List<string>()},
                    {"IdentityServer", new List<string> { "openid", "profile", "UserServicesApi" }}
                };

                options.AddSecurityRequirement(securityRequirement);
                options.OperationFilter<AuthorizeCheckOperationFilter>();// 添加IdentityServer4认证过滤
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();

            app.UseSwagger()
               .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "esoftor web api V1");
                    options.OAuthClientId("swaggerClient");//客服端名称
                });
        }
    }
}