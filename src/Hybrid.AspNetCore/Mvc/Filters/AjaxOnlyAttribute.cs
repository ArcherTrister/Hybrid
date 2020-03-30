﻿// -----------------------------------------------------------------------
//  <copyright file="AjaxOnlyAttribute.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-01 18:11</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System;

namespace Hybrid.AspNetCore.Mvc.Filters
{
    /// <summary>
    /// 限制当前功能只允许以Ajax的方式来访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.IsAjaxRequest())
            {
                context.Result = new ContentResult()
                {
                    Content = "当前功能只支持使用Ajax的方式来调用。"
                };
            }
        }
    }
}