// -----------------------------------------------------------------------
//  <copyright file="EndpointsPack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-09-29 13:10</last-date>
// -----------------------------------------------------------------------


using System.ComponentModel;

using Microsoft.AspNetCore.Routing;


namespace Hybrid.AspNetCore.Routing
{
    /// <summary>
    /// Endpoints模块
    /// </summary>
    [Description("Endpoints模块")]
    public class EndpointsPack : EndpointsPackBase
    {
        /// <summary>
        /// 重写以配置SignalR的终结点
        /// </summary>
        protected override IEndpointRouteBuilder SignalREndpoints(IEndpointRouteBuilder endpoints)
        {
            // 在这实现Hub的路由映射
            // 例如：endpoints.MapHub<ChatHub>();
            return endpoints;
        }
    }
}
