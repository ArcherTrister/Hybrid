// -----------------------------------------------------------------------
//  <copyright file="Startup.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore;
using Hybrid.AspNetCore.Middlewares;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Polly;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace Hybrid.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("Identityserver4Client", options =>
            {
                options.BaseAddress = new Uri(Configuration["Hybrid:Ids:Authority"]);
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                options.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            })
            .AddTransientHttpErrorPolicy(
                    p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)
                )
            )
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddHttpClient("requestClient",
            options =>
            {
                //options.BaseAddress = new Uri(Configuration["QMWallet:GateWayUrl"]);
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                options.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            })
            .AddTransientHttpErrorPolicy(
                    p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)
                )
            )
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            //Add-Migration Init -Verbose -o Data/Migrations/Application
            services.AddHybrid<AspHybridModuleManager>();

            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new Info { Title = "hybrid web api", Version = "v1" });
            //    Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(file =>
            //    {
            //        options.IncludeXmlComments(file);
            //    });
            //    //向生成的Swagger添加一个或多个“securityDefinitions”，用于API的登录校验
            //    //权限Token
            //    options.AddSecurityDefinition("Bearer", new ApiKeyScheme
            //    {
            //        Description = "JWT Bearer 授权 \"Authorization:     Bearer+空格+token\"",
            //        Name = "Authorization",
            //        In = "header",
            //        Type = "apiKey"
            //    });
            //    options.AddSecurityDefinition("IdentityServer",
            //        new OAuth2Scheme
            //        {
            //            Type = "oauth2",
            //            Flow = "implicit",
            //            AuthorizationUrl = "http://localhost:5002/connect/authorize",
            //            Scopes = new Dictionary<string, string>
            //            {
            //                { HybridConsts.LocalApi.ScopeName, "IdentityServerApi授权" }
            //            }
            //        });

            //    var securityRequirement = new Dictionary<string, IEnumerable<string>>
            //    {
            //        {"Bearer", new List<string>()},
            //        {"IdentityServer", new List<string> { "openid", "profile", "UserServicesApi" }}
            //    };

            //    options.AddSecurityRequirement(securityRequirement);
            //    options.OperationFilter<AuthorizeCheckOperationFilter>();// 添加IdentityServer4认证过滤
            //});

            services.AddMvcCore().AddApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc($"v1", new OpenApiInfo() { Title = "hybrid web api", Version = "v1" });

                Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(file =>
                {
                    options.IncludeXmlComments(file);
                });

                //向生成的Swagger添加一个或多个“securityDefinitions”，用于API的登录校验
                //权限Token
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
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
                            AuthorizationUrl = new Uri("http://localhost:5002/connect/authorize"),
                            TokenUrl = new Uri("http://localhost:5002/connect/token"),
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
                              Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                          },
                          new List<string>()
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    //app.UseDatabaseErrorPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app
                //.UseMiddleware<NoFoundHandlerMiddleware>()
                .UseMiddleware<ExceptionHandlerMiddleware>()
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseHybrid();

            app.UseSwagger().
                UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "hybrid web api V1");
                    options.OAuthClientId("swaggerClient");//客服端名称
                });
        }
    }
}