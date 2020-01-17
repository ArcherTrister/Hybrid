// -----------------------------------------------------------------------
//  <copyright file="HttpContextExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-01 20:39:00</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Http;

using System.Linq;

namespace Hybrid.AspNetCore.Extensions
{
    /// <summary>
    /// HttpContext扩展方法
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        public static string GetClientIp(this HttpContext context)
        {
            string ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
    }
}