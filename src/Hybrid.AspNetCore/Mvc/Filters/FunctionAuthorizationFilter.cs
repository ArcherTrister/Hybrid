// -----------------------------------------------------------------------
//  <copyright file="FunctionAuthorizationFilter" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-12 15:11:41</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Extensions;
using Hybrid.AspNetCore.UI;
using Hybrid.Authorization;
using Hybrid.Authorization.Functions;
using Hybrid.Data;
using Hybrid.Security;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Security.Principal;

namespace Hybrid.AspNetCore.Mvc.Filters
{
    /// <summary>
    /// 功能权限授权验证
    /// </summary>
    public class FunctionAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        #region NoApi

        /// <summary>
        /// Called early in the filter pipeline to confirm request is authorized.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext" />.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Check.NotNull(context, nameof(context));
            IFunction function = context.GetExecuteFunction();
            AuthorizationResult result = AuthorizeCore(context, function);
            if (!result.IsOk)
            {
                HandleUnauthorizedRequest(context, result);
            }
        }

        /// <summary>
        /// 重写以实现功能权限的核心验证逻辑
        /// </summary>
        /// <param name="context">权限过滤器上下文</param>
        /// <param name="function">要验证的功能</param>
        /// <returns>权限验证结果</returns>
        protected virtual AuthorizationResult AuthorizeCore(AuthorizationFilterContext context, IFunction function)
        {
            IPrincipal user = context.HttpContext.User;
            IServiceProvider provider = context.HttpContext.RequestServices;
            IFunctionAuthorization authorization = provider.GetService<IFunctionAuthorization>();
            AuthorizationResult result = authorization.Authorize(function, user);
            return result;
        }

        /// <summary>
        /// 重写以实现授权未通过的处理逻辑
        /// </summary>
        /// <param name="context">权限验证上下文</param>
        /// <param name="result">权限检查结果</param>
        protected virtual void HandleUnauthorizedRequest(AuthorizationFilterContext context, AuthorizationResult result)
        {
            //Json方式请求，返回AjaxResult
            bool isJsRequest = context.HttpContext.Request.IsAjaxRequest() || context.HttpContext.Request.IsJsonContextType();

            AuthorizationStatus status = result.ResultType;
            switch (status)
            {
                case AuthorizationStatus.OK:
                    break;

                case AuthorizationStatus.Unauthorized:
                    context.Result = isJsRequest
                        ? (IActionResult)new JsonResult(new AjaxResult(result.Message, AjaxResultType.UnAuth))
                        : new UnauthorizedResult();
                    break;

                case AuthorizationStatus.Forbidden:
                    context.Result = isJsRequest
                        ? (IActionResult)new JsonResult(new AjaxResult(result.Message, AjaxResultType.Forbidden))
                        : new StatusCodeResult(403);
                    break;

                case AuthorizationStatus.NoFound:
                    context.Result = isJsRequest
                        ? (IActionResult)new JsonResult(new AjaxResult(result.Message, AjaxResultType.NoFound))
                        : new StatusCodeResult(404);
                    break;

                case AuthorizationStatus.Locked:
                    context.Result = isJsRequest
                        ? (IActionResult)new JsonResult(new AjaxResult(result.Message, AjaxResultType.Locked))
                        : new StatusCodeResult(423);
                    break;

                case AuthorizationStatus.Error:
                    context.Result = isJsRequest
                        ? (IActionResult)new JsonResult(new AjaxResult(result.Message, AjaxResultType.Error))
                        : new StatusCodeResult(500);
                    break;
            }
            if (isJsRequest)
            {
                context.HttpContext.Response.StatusCode = 200;
            }
        }

        #endregion NoApi

        #region Api

        ///// <summary>
        ///// Called early in the filter pipeline to confirm request is authorized.
        ///// </summary>
        ///// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext" />.</param>
        //public void OnAuthorization(AuthorizationFilterContext context)
        //{
        //    //Check.NotNull(context, nameof(context));
        //    IServiceProvider provider = context.HttpContext.RequestServices;
        //    IFunction function = context.GetExecuteFunction();
        //    AuthorizationResult result = AuthorizeCore(context, provider, function);
        //    if (!result.IsOk)
        //    {
        //        HandleUnauthorizedRequest(context, provider, result);
        //    }
        //}

        ///// <summary>
        ///// 重写以实现功能权限的核心验证逻辑
        ///// </summary>
        ///// <param name="context">权限过滤器上下文</param>
        ///// <param name="function">要验证的功能</param>
        ///// <returns>权限验证结果</returns>
        //protected virtual AuthorizationResult AuthorizeCore(AuthorizationFilterContext context, IServiceProvider provider, IFunction function)
        //{
        //    IPrincipal user = context.HttpContext.User;
        //    IFunctionAuthorization authorization = provider.GetService<IFunctionAuthorization>();
        //    AuthorizationResult result = authorization.Authorize(function, user);
        //    return result;
        //}

        ///// <summary>
        ///// 重写以实现授权未通过的处理逻辑
        ///// </summary>
        ///// <param name="context">权限验证上下文</param>
        ///// <param name="result">权限检查结果</param>
        //protected virtual void HandleUnauthorizedRequest(AuthorizationFilterContext context, IServiceProvider provider, AuthorizationResult result)
        //{
        //    //if (result.IsApi)
        //    //{
        //    //    //Json方式请求，返回AjaxResult
        //    //    bool isJsRequest = context.HttpContext.Request.IsAjaxRequest() || context.HttpContext.Request.IsJsonContextType();

        //    //    AuthorizationStatus status = result.ResultType;
        //    //    switch (status)
        //    //    {
        //    //        case AuthorizationStatus.OK:
        //    //            break;
        //    //        case AuthorizationStatus.Unauthorized:
        //    //            context.Result = isJsRequest
        //    //                ? (IActionResult)new JsonResult(new AjaxResult(result.Message, AjaxResultType.UnAuth))
        //    //                : new UnauthorizedResult();
        //    //            break;

        //    //        case AuthorizationStatus.Forbidden:
        //    //            context.Result = isJsRequest
        //    //                ? (IActionResult)new JsonResult(new AjaxResult(result.Message, AjaxResultType.Forbidden))
        //    //                : new StatusCodeResult(403);
        //    //            break;

        //    //        case AuthorizationStatus.NoFound:
        //    //            context.Result = isJsRequest
        //    //                ? (IActionResult)new JsonResult(new AjaxResult(result.Message, AjaxResultType.NoFound))
        //    //                : new StatusCodeResult(404);
        //    //            break;

        //    //        case AuthorizationStatus.Locked:
        //    //            context.Result = isJsRequest
        //    //                ? (IActionResult)new JsonResult(new AjaxResult(result.Message, AjaxResultType.Locked))
        //    //                : new StatusCodeResult(423);
        //    //            break;

        //    //        case AuthorizationStatus.Error:
        //    //            context.Result = isJsRequest
        //    //                ? (IActionResult)new JsonResult(new AjaxResult(result.Message, AjaxResultType.Error))
        //    //                : new StatusCodeResult(500);
        //    //            break;
        //    //    }
        //    //    if (isJsRequest)
        //    //    {
        //    //        context.HttpContext.Response.StatusCode = 200;
        //    //    }
        //    //}
        //    //else {
        //    //    //IdentityServer4Options options = provider.GetHybridOptions().IdentityServer;
        //    //    //string returnUrl = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
        //    //    //string loginUrl = options.LoginUrl;
        //    //    //if (!loginUrl.IsLocalUrl())
        //    //    //{
        //    //    //    returnUrl = context.HttpContext.GetServerHost().EnsureTrailingSlash() + returnUrl.RemoveLeadingSlash();
        //    //    //}
        //    //    //var url = loginUrl.AddQueryString("ReturnUrl", returnUrl);
        //    //    //context.HttpContext.Response.RedirectToAbsoluteUrl(url);
        //    //}
        //}

        #endregion Api
    }
}