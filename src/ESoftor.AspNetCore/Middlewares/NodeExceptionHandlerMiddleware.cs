// -----------------------------------------------------------------------
//  <copyright file="NodeExceptionHandlerMiddleware.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.AspNetCore.Extensions;
using ESoftor.AspNetCore.UI;
using ESoftor.Data;
using ESoftor.Json;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace ESoftor.AspNetCore.Middlewares
{
    /// <summary>
    /// Node技术异常处理中间件
    /// </summary>
    public class NodeExceptionHandlerMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<NodeExceptionHandlerMiddleware> _logger;

        /// <summary>
        /// 初始化一个<see cref="NodeExceptionHandlerMiddleware"/>类型的新实例
        /// </summary>
        public NodeExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            Check.NotNull(next, nameof(next));

            _next = next;
            _logger = loggerFactory.CreateLogger<NodeExceptionHandlerMiddleware>();
        }

        /// <summary>
        /// 执行中间件拦截逻辑
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
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
                    context.Response.StatusCode = 500;
                    context.Response.Clear();
                    context.Response.ContentType = "application/json; charset=utf-8";
                    await context.Response.WriteAsync(new AjaxResult(ex.Message, AjaxResultType.Error).ToJsonString());
                    return;
                }
                throw;
            }
        }
    }
}