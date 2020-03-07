// -----------------------------------------------------------------------
//  <copyright file="NodeExceptionHandlerMiddleware.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Extensions;
using Hybrid.AspNetCore.UI;
using Hybrid.Data;
using Hybrid.Json;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace Hybrid.AspNetCore.Middlewares
{
    /// <summary>
    /// Node技术异常处理中间件
    /// </summary>
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        //private static int[] IgnoreStatusCode = new int[] { 200, 302 };
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        /// <summary>
        /// 初始化一个<see cref="ExceptionHandlerMiddleware"/>类型的新实例
        /// </summary>
        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
        }

        /// <summary>
        /// 执行中间件拦截逻辑
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(), ex, ex.Message);
                if (context.Request.IsAjaxRequest() || context.Request.IsJsonContextType())
                {
                    if (context.Response.HasStarted)
                    {
                        return;
                    }
                    await HandleExceptionAsync(context, 500, "服务提供异常，请重试或联系管理员", AjaxResultType.Error);
                    return;
                }
                throw;
            }
            finally
            {
                int statusCode = context.Response.StatusCode;
                //!IgnoreStatusCode.AsSpan().Contains(statusCode)
                if (statusCode >= 400)
                {
                    AjaxResultType resultType;
                    string msg;
                    switch (statusCode)
                    {
                        case 400:
                            msg = "请求错误";
                            resultType = AjaxResultType.RequestError;
                            break;

                        case 401:
                            msg = "用户未登录，请先登录";
                            resultType = AjaxResultType.UnAuth;
                            break;

                        case 403:
                            msg = "已登录，但权限不足";
                            resultType = AjaxResultType.Forbidden;
                            break;

                        case 404:
                            msg = "资源未找到，无法访问";
                            resultType = AjaxResultType.NoFound;
                            break;

                        case 405:
                            msg = "方法被禁用";
                            resultType = AjaxResultType.MethodDisabled;
                            break;

                        case 406:
                            msg = "不支持此格式";
                            resultType = AjaxResultType.NoSupport;
                            break;

                        case 423:
                            msg = "资源被锁定，无法访问";
                            resultType = AjaxResultType.Locked;
                            break;

                        default:
                            msg = "服务器异常，请重试或联系管理员";
                            resultType = AjaxResultType.Error;
                            break;
                    }
                    await HandleExceptionAsync(context, statusCode, msg, resultType);
                }
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg, AjaxResultType resultType)
        {
            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json; charset=utf-8";
            return context.Response.WriteAsync(new AjaxResult(content: msg, ajaxResultType: resultType, unAuthorizedRequest: statusCode.Equals(401)).ToJsonString());
        }
    }
}