// -----------------------------------------------------------------------
//  <copyright file="Startup.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-03-26 21:47</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore;
using Hybrid.AspNetCore.DynamicWebApi;
using Hybrid.AspNetCore.Routing;
using Hybrid.AspNetCore.SignalR;
using Hybrid.AutoMapper;

//using Hybrid.Log4Net;
using Hybrid.NLog;
using Hybrid.Quartz;
using Hybrid.Swagger;

using LeXun.Demo.Authorization;
using LeXun.Demo.Identity;
using LeXun.Demo.Systems;
using LeXun.Demo.Web.Startups;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LeXun.Demo.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHybrid()
                //.AddPack<Log4NetPack>()
                .AddPack<NLogPack>()
                .AddPack<AutoMapperPack>()
                .AddPack<EndpointsPack>()
                .AddPack<SignalRPack>()
                .AddPack<SwaggerPack>()
                //.AddPack<RedisPack>()
                .AddPack<QuartzPack>()
                .AddPack<IdentityServer4Pack>()
                .AddPack<FunctionAuthorizationPack>()
                .AddPack<DataAuthorizationPack>()
                .AddPack<DynamicWebApiPack>()
                .AddPack<SqlServerDefaultDbContextMigrationPack>()
                .AddPack<AuditPack>();

            ////Add-Migration Init -Verbose -o Data/Migrations
            //services.AddHybrid<AspHybridModuleManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/#/500");
                app.UseHsts();
                //app.UseHttpsRedirection(); // 启用HTTPS
            }

            //app.UseMiddleware<HostHttpCryptoMiddleware>();
            //app.UseMiddleware<JsonNoFoundHandlerMiddleware>();
            app.UseMiddleware<JsonExceptionHandlerMiddleware>();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHybrid();
            //app.UseAutoHybrid();
        }
    }
}