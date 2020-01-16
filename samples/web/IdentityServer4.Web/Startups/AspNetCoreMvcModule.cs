﻿// -----------------------------------------------------------------------
//  <copyright file="AspNetCoreMvcModule.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Mvc;

using Microsoft.Extensions.DependencyInjection;

using System.ComponentModel;

namespace Hybrid.Web.Startups
{
    /// <summary>
    /// MVC模块，此模块需要在Identity之后启动
    /// </summary>
    [Description("MVC模块")]
    public class AspNetCoreMvcModule : MvcModuleBase
    {
        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 0;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            //services.AddRazorPages();
            //services.AddHttpsRedirection(opts => opts.HttpsPort = 443);
            return base.AddServices(services);
        }
    }
}