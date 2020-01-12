// -----------------------------------------------------------------------
//  <copyright file="ServiceProviderExtensions.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace ESoftor.AspNetCore.Extensions
{
    /// <summary>
    /// 服务解析扩展
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// 获取HttpContext实例
        /// </summary>
        public static HttpContext HttpContext(this IServiceProvider provider)
        {
            IHttpContextAccessor accessor = provider.GetService<IHttpContextAccessor>();
            return accessor?.HttpContext;
        }

        /// <summary>
        /// 当前业务是否处于HttpRequest中
        /// </summary>
        public static bool InHttpRequest(this IServiceProvider provider)
        {
            var context = provider.HttpContext();
            return context != null;
        }
    }
}