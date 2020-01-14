﻿using Microsoft.AspNetCore.Mvc.Routing;

using System;
using System.Net.Http;

namespace Test.Web.Attributes
{
    /// <summary>
    /// 表示Delete请求
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpDeleteAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <exception cref="ArgumentNullException"></exception>
        public HttpDeleteAttribute(string path = "")
            : base(HttpMethod.Delete, path)
        {
        }
    }
}