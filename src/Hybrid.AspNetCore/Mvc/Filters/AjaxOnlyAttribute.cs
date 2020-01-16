// -----------------------------------------------------------------------
//  <copyright file="AjaxOnlyAttribute.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Extensions;

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