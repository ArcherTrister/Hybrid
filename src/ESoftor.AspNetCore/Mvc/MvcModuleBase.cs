// -----------------------------------------------------------------------
//  <copyright file="MvcModuleBase.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.AspNetCore.Mvc.Filters;
using ESoftor.Core.Modules;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace ESoftor.AspNetCore.Mvc
{
    /// <summary>
    /// Mvc模块基类
    /// </summary>
    [DependsOnModules(typeof(AspNetCoreModule))]
    public abstract class MvcModuleBase : AspESoftorModule
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override ModuleLevel Level => ModuleLevel.Application;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services = AddCors(services);



            var builder = services.AddControllersWithViews(options =>
            {
                //    //options.Conventions.Add(new DashedRoutingConvention());
                //    //options.Filters.Add(new OnlineUserAuthorizationFilter()); // 构建在线用户信息
                options.Filters.Add(new FunctionAuthorizationFilter()); // 全局功能权限过滤器
                options.Filters.Add(new OperateAuditFilter());
                options.Filters.Add(new MvcUnitOfWorkFilter());
                options.Filters.Add(new PageUnitOfWorkFilter());
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            //services.AddRazorPages();

            //services.AddScoped<OperateAuditFilter>();
            services.AddScoped<MvcUnitOfWorkFilter>();
            services.AddScoped<PageUnitOfWorkFilter>();
            services.AddHttpsRedirection(opts => opts.HttpsPort = 443);
            services.AddDistributedMemoryCache();

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        public override void UseModule(IApplicationBuilder app)
        {
            app.UseRouting();
            UseCors(app);
            //app.UseMvcWithAreaRoute();
            IsEnabled = true;
        }

        /// <summary>
        /// 重写以实现添加Cors服务
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected virtual IServiceCollection AddCors(IServiceCollection services)
        {
            return services;
        }

        /// <summary>
        /// 重写以应用Cors
        /// </summary>
        protected virtual IApplicationBuilder UseCors(IApplicationBuilder app)
        {
            return app;
        }
    }
}