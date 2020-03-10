﻿// -----------------------------------------------------------------------
//  <copyright file="HttpRequestExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-01 20:39:00</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;

using Microsoft.AspNetCore.Http;

using System;
using System.Linq;

namespace Hybrid.AspNetCore.Extensions
{
    /// <summary>
    /// HttpContext扩展方法
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 确定指定的 HTTP 请求是否为 AJAX 请求。
        /// </summary>
        ///
        /// <returns>
        /// 如果指定的 HTTP 请求是 AJAX 请求，则为 true；否则为 false。
        /// </returns>
        /// <param name="request">HTTP 请求。</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="request"/> 参数为 null（在 Visual Basic 中为 Nothing）。</exception>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            //Check.NotNull(request, nameof(request));

            return string.Equals(request.Query["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal)
                || string.Equals(request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal);
        }

        /// <summary>
        /// 确定指定的 HTTP 请求的 ContextType 是否为 Json 方式
        /// </summary>
        public static bool IsJsonContextType(this HttpRequest request)
        {
            //Check.NotNull(request, nameof(request));
            bool flag = request.Headers?["Content-Type"].ToString().IndexOf("application/json", StringComparison.OrdinalIgnoreCase) > -1
                || request.Headers?["Content-Type"].ToString().IndexOf("text/json", StringComparison.OrdinalIgnoreCase) > -1;
            if (flag)
            {
                return true;
            }
            flag = request.Headers?["Accept"].ToString().IndexOf("application/json", StringComparison.OrdinalIgnoreCase) > -1
                || request.Headers?["Accept"].ToString().IndexOf("text/json", StringComparison.OrdinalIgnoreCase) > -1;
            return flag;
        }

        /// <summary>
        /// 获取<see cref="HttpRequest"/>的请求数据
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <param name="key">要获取数据的键名</param>
        /// <returns></returns>
        public static string Params(this HttpRequest request, string key)
        {
            if (request.Query.ContainsKey(key))
            {
                return request.Query[key];
            }
            if (request.HasFormContentType)
            {
                return request.Form[key];
            }
            return null;
        }
    }
}