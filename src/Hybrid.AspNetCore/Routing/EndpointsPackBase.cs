﻿// -----------------------------------------------------------------------
//  <copyright file="EndpointsPack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-09-29 12:43</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Packs;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Hybrid.AspNetCore.Routing
{
    /// <summary>
    /// Endpoints模块，处理MVC和SignalR的路由结点配置
    /// </summary>
    [DependsOnPacks(typeof(AspNetCorePack))]
    public abstract class EndpointsPackBase : AspHybridPack
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Application;

        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，同一级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 99;

        /// <summary>
        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        public override void UsePack(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints = MvcEndpoints(endpoints);
                SignalREndpoints(endpoints);
                OtherEndpoints(endpoints);
            });
        }

        /// <summary>
        /// 重写以配置MVC的终结点
        /// </summary>
        /// <param name="endpoints">终结点路由配置</param>
        protected virtual IEndpointRouteBuilder MvcEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllersWithAreaRoute();

            return endpoints;
        }

        /// <summary>
        /// 重写以配置SignalR的终结点
        /// </summary>
        /// <param name="endpoints">终结点路由配置</param>
        protected virtual IEndpointRouteBuilder SignalREndpoints(IEndpointRouteBuilder endpoints)
        {
            return endpoints;
        }

        /// <summary>
        /// 重写以配置其他终结点
        /// </summary>
        /// <param name="endpoints">终结点路由配置</param>
        /// <returns></returns>
        protected virtual IEndpointRouteBuilder OtherEndpoints(IEndpointRouteBuilder endpoints)
        {
            return endpoints;
        }
    }
}