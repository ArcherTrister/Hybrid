// -----------------------------------------------------------------------
//  <copyright file="Startup.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.AspNetCore;
using ESoftor.AspNetCore.Middlewares;
using ESoftor.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ESoftor.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Add-Migration Init -Verbose -o Data/Migrations
            services.AddESoftor<AspESoftorModuleManager>();

            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new Info { Title = "esoftor web api", Version = "v1" });
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
            //                { ESoftorConstants.LocalApi.ScopeName, "IdentityServerApi授权" }
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
                options.SwaggerDoc($"v1", new OpenApiInfo() { Title = "esoftor web api", Version = "v1" });

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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCookiePolicy()
                //.UseMiddleware<NodeNoFoundHandlerMiddleware>()
                .UseMiddleware<NodeExceptionHandlerMiddleware>()
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseESoftor();

            app.UseSwagger().
                UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "esoftor web api V1");
                    options.OAuthClientId("swaggerClient");//客服端名称
                });
        }
    }
}