// -----------------------------------------------------------------------
//  <copyright file="JsonExceptionHandlerMiddleware.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-05-12 17:51</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.UI;
using Hybrid.Data;
using Hybrid.Json;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace Hybrid.AspNetCore
{
    /// <summary>
    /// Json技术异常处理中间件
    /// </summary>
    public class JsonExceptionHandlerMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JsonExceptionHandlerMiddleware> _logger;

        /// <summary>
        /// 初始化一个<see cref="JsonExceptionHandlerMiddleware"/>类型的新实例
        /// </summary>
        public JsonExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<JsonExceptionHandlerMiddleware>();
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
                    await HandleWebApiExceptionAsync(context, 500, "服务提供异常，请重试或联系管理员", AjaxResultType.Error);
                    return;
                }
                await HandleMvcExceptionAsync(context);
            }
            finally
            {
                int statusCode = context.Response.StatusCode;
                //!IgnoreStatusCode.AsSpan().Contains(statusCode)
                if (statusCode >= 400)
                {
                    if (context.Request.IsAjaxRequest() || context.Request.IsJsonContextType())
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
                        await HandleWebApiExceptionAsync(context, statusCode, msg, resultType);
                    }
                    else
                    {
                        await HandleMvcExceptionAsync(context);
                    }
                }
            }
        }

        private static Task HandleWebApiExceptionAsync(HttpContext context, int statusCode, string msg, AjaxResultType resultType)
        {
            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json; charset=utf-8";
            return context.Response.WriteAsync(new AjaxResult(content: msg, ajaxResultType: resultType, unAuthorizedRequest: statusCode.Equals(401)).ToJsonString());
        }

        private const string errorHtml = "<!doctype html><html lang=\"en\"><head><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1, shrink-to-fit=no\">" +
            "<link rel=\"stylesheet\" href=\"https://cdn.bootcss.com/bootstrap/4.0.0/css/bootstrap.min.css\" integrity=\"sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm\" crossorigin=\"anonymous\">" +
            "<title>Error</title></head><body>" +
            "<div class=\"row justify-content-center\" style=\"height: 400px\"><div class=\"align-self-center\"><p class=\"text-danger\">Sorry, an error has occured !</p><p><a href = \"/\" class=\"btn btn-primary btn-lg\">Take Me Home</a></p></div></div>" +
            "<script src=\"https://cdn.bootcss.com/jquery/3.2.1/jquery.slim.min.js\" integrity=\"sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN\" crossorigin=\"anonymous\"></script>" +
            "<script src=\"https://cdn.bootcss.com/popper.js/1.12.9/umd/popper.min.js\" integrity=\"sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q\" crossorigin=\"anonymous\"></script>" +
            "<script src=\"https://cdn.bootcss.com/bootstrap/4.0.0/js/bootstrap.min.js\" integrity=\"sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl\" crossorigin=\"anonymous\"></script>" +
            "</body></html>";

        private static Task HandleMvcExceptionAsync(HttpContext context)
        {
            context.Response.Clear();
            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/html; charset=utf-8";
            return context.Response.WriteAsync(errorHtml);
        }
    }
}