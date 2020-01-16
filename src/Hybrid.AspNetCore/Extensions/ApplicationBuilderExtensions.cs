// -----------------------------------------------------------------------
//  <copyright file="ApplicationBuilderExtensions.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore;
using Hybrid.Core.Modules;
using Hybrid.Exceptions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// <see cref="IApplicationBuilder"/>辅助扩展方法
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Hybrid框架初始化，适用于AspNetCore环境
        /// </summary>
        public static IApplicationBuilder UseESoftor(this IApplicationBuilder app)
        {
            IServiceProvider provider = app.ApplicationServices;
            if (!(provider.GetService<IESoftorModuleManager>() is IAspUseModule aspModuleManager))
            {
                throw new ESoftorException("接口 IESoftorModuleManager 的注入类型不正确，该类型应同时实现接口 IAspUseModule");
            }
            aspModuleManager.UseModule(app);

            return app;
        }

        /// <summary>
        /// 添加MVC并Area路由支持
        /// </summary>
        public static IApplicationBuilder UseMvcWithAreaRoute(this IApplicationBuilder app, bool area = true)
        {
            return app.UseMvc(builder =>
            {
                if (area)
                {
                    builder.MapRoute("area", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                }
                builder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }

        /// <summary>
        /// 添加Endpoint并Area路由支持
        /// </summary>
        public static IEndpointRouteBuilder MvcEndpointsWithAreaRoute(this IEndpointRouteBuilder endpoints, bool area = true)
        {
            if (area)
            {
                endpoints.MapControllerRoute("area", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            }

            endpoints.MapDefaultControllerRoute();
            return endpoints;
        }
    }
}