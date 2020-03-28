// -----------------------------------------------------------------------
//  <copyright file="EndpointsModule.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Routing;

using Microsoft.AspNetCore.Routing;

using System.ComponentModel;

namespace Hybrid.Web.Startups
{
    /// <summary>
    /// Endpoints模块
    /// </summary>
    [Description("Endpoints模块")]
    public class EndpointsModule : EndpointsModuleBase
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

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="endpoints"></param>
        ///// <returns></returns>
        //protected override IEndpointRouteBuilder OtherEndpoints(IEndpointRouteBuilder endpoints)
        //{
        //    endpoints.MapControllerRoute("IdentityServer", "{area:exists}/{controller=IdentityServer}/{action=Index}/{id?}");
        //    return base.OtherEndpoints(endpoints);
        //}
    }
}