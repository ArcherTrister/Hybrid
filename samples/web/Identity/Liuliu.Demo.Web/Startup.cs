// -----------------------------------------------------------------------
//  <copyright file="Startup.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-03-26 21:47</last-date>
// -----------------------------------------------------------------------

using Liuliu.Demo.Authorization;
using Liuliu.Demo.Identity;
using Liuliu.Demo.Systems;
using Liuliu.Demo.Web.Startups;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Hybrid.AspNetCore;
using Hybrid.AspNetCore.Routing;
using Hybrid.AutoMapper;
//using Hybrid.Log4Net;
using Hybrid.NLog;
using Hybrid.Swagger;


namespace Liuliu.Demo.Web
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
                .AddPack<SwaggerPack>()
                //.AddPack<RedisPack>()
                .AddPack<AuthenticationPack>()
                .AddPack<FunctionAuthorizationPack>()
                .AddPack<DataAuthorizationPack>()
                .AddPack<SqlServerDefaultDbContextMigrationPack>()
                .AddPack<AuditPack>();
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
        }
    }
}