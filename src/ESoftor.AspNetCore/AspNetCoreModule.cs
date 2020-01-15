// -----------------------------------------------------------------------
//  <copyright file="AspNetCoreModule.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Modules;

using Microsoft.Extensions.DependencyInjection;

using System.ComponentModel;

namespace ESoftor.AspNetCore
{
    /// <summary>
    /// AspNetCore模块
    /// </summary>
    [Description("AspNetCore模块")]
    public class AspNetCoreModule : ESoftorModule
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override ModuleLevel Level => ModuleLevel.Core;

        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动
        /// </summary>
        public override int Order => 2;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            //TODO:IPrincipal
            ////注入当前用户，替换Thread.CurrentPrincipal的作用
            //services.AddTransient<IPrincipal>(provider =>
            //{
            //    IHttpContextAccessor accessor = provider.GetService<IHttpContextAccessor>();
            //    return accessor?.HttpContext?.User;
            //});

            return services;
        }
    }
}