﻿// -----------------------------------------------------------------------
//  <copyright file="MvcFunctionAuthorizationHandler.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-02-11 14:12</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.Functions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using System.Security.Claims;
using System.Threading.Tasks;

namespace Hybrid.Authorization
{
    /// <summary>
    /// 功能授权处理器
    /// </summary>
    public class FunctionAuthorizationHandler : AuthorizationHandler<FunctionRequirement>
    {
        private readonly IFunctionAuthorization _functionAuthorization;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 初始化一个<see cref="FunctionAuthorizationHandler"/>类型的新实例
        /// </summary>
        public FunctionAuthorizationHandler(IFunctionAuthorization functionAuthorization, IHttpContextAccessor httpContextAccessor)
        {
            _functionAuthorization = functionAuthorization;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 如果根据特定要求允许授权，则作出决定。
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, FunctionRequirement requirement)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (context.Resource is RouteEndpoint endpoint)
            {
                IFunction function = endpoint.GetExecuteFunction(httpContext);
                AuthorizationResult result = AuthorizeCore(context, function);
                if (result.IsOk)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 重写以实现功能权限的核心验证逻辑
        /// </summary>
        /// <param name="context">权限过滤器上下文</param>
        /// <param name="function">要验证的功能</param>
        /// <returns>权限验证结果</returns>
        protected virtual AuthorizationResult AuthorizeCore(AuthorizationHandlerContext context, IFunction function)
        {
            ClaimsPrincipal user = context.User;
            AuthorizationResult result = _functionAuthorization.Authorize(function, user);
            return result;
        }
    }
}