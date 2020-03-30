// -----------------------------------------------------------------------
//  <copyright file="RouteEndpointExtensions.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-02-12 13:14</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.Functions;
using Hybrid.Dependency;
using Hybrid.Exceptions;
using Hybrid.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace Hybrid.Authorization
{
    /// <summary>
    /// <see cref="RouteEndpoint"/>扩展方法
    /// </summary>
    public static class EndpointExtensions
    {
        /// <summary>
        /// 获取正在执行的Action相关功能信息
        /// </summary>
        public static IFunction GetExecuteFunction(this RouteEndpoint endpoint, HttpContext context)
        {
            IServiceProvider provider = context.RequestServices;
            ScopedDictionary dict = provider.GetService<ScopedDictionary>();
            if (dict.Function != null)
            {
                return dict.Function;
            }

            string area = endpoint.GetAreaName(),
                controller = endpoint.GetControllerName(),
                action = endpoint.GetActionName();
            IFunctionHandler functionHandler = provider.GetService<IFunctionHandler>();
            if (functionHandler == null)
            {
                throw new HybridException("获取正在执行的功能时 IFunctionHandler 无法解析");
            }

            IFunction function = functionHandler.GetFunction(area, controller, action);
            if (function != null)
            {
                dict.Function = function;
            }

            return function;
        }

        /// <summary>
        /// 获取Area名
        /// </summary>
        public static string GetAreaName(this RouteEndpoint endpoint)
        {
            string area = null;
            if (endpoint.RoutePattern.Defaults.TryGetValue("area", out object value))
            {
                area = (string)value;
                if (area.IsNullOrWhiteSpace())
                {
                    area = null;
                }
            }

            return area;
        }

        /// <summary>
        /// 获取Controller名
        /// </summary>
        public static string GetControllerName(this RouteEndpoint endpoint)
        {
            return endpoint.RoutePattern.Defaults["controller"].ToString();
        }

        /// <summary>
        /// 获取Action名
        /// </summary>
        public static string GetActionName(this RouteEndpoint endpoint)
        {
            return endpoint.RoutePattern.Defaults["action"].ToString();
        }

        /// <summary>
        /// 需要Hybrid授权
        /// </summary>
        public static ControllerActionEndpointConventionBuilder RequireHybridAuthorization(this ControllerActionEndpointConventionBuilder builder)
        {
            return builder.RequireAuthorization(FunctionRequirement.HybridPolicy);
        }
    }
}