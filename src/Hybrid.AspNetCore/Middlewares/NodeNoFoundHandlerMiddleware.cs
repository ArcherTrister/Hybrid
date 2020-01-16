// -----------------------------------------------------------------------
//  <copyright file="NodeNoFoundHandlerMiddleware.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;

using Microsoft.AspNetCore.Http;

using System;
using System.IO;
using System.Threading.Tasks;

namespace Hybrid.AspNetCore.Middlewares
{
    /// <summary>
    /// Node前端技术404返回index.html中间件
    /// </summary>
    public class NodeNoFoundHandlerMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 初始化一个<see cref="NodeNoFoundHandlerMiddleware"/>类型的新实例
        /// </summary>
        public NodeNoFoundHandlerMiddleware(RequestDelegate next)
        {
            Check.NotNull(next, nameof(next));

            _next = next;
        }

        /// <summary>
        /// 执行中间件拦截逻辑
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value)
                && !context.Request.Path.Value.StartsWith("/api/", StringComparison.OrdinalIgnoreCase))
            {
                context.Request.Path = "/index.html";
                await _next(context);
            }
        }
    }
}