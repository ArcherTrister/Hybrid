﻿// -----------------------------------------------------------------------
//  <copyright file="SystemPack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-25 21:12</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Packs;
using Hybrid.Core.Systems;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System.ComponentModel;

namespace Hybrid.Systems
{
    /// <summary>
    /// 系统信息模块
    /// </summary>
    [Description("系统信息模块")]
    public class SystemPack : HybridPack
    {
        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 0;

        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Application;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.TryAddScoped<IKeyValueStore, KeyValueStore>();

            return services;
        }

        ///// <summary>
        ///// 将模块服务添加到依赖注入服务容器中【自动模式】
        ///// </summary>
        ///// <param name="services">依赖注入服务容器</param>
        ///// <returns></returns>
        //public override IServiceCollection AddAutoServices(IServiceCollection services)
        //{
        //    services.TryAddScoped<IKeyValueStore, KeyValueStore>();

        //    return services;
        //}
    }
}