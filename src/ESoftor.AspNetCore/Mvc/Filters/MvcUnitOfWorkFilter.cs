// -----------------------------------------------------------------------
//  <copyright file="MvcUnitOfWorkFilter.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.AspNetCore.Extensions;
using ESoftor.AspNetCore.UI;
using ESoftor.Data;
using ESoftor.Dependency;
using ESoftor.Domain.Uow;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;

namespace ESoftor.AspNetCore.Mvc.Filters
{
    internal class MvcUnitOfWorkFilter : IActionFilter
    {
        /// <summary>
        /// Called before the action executes, after model binding is complete.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        { }

        /// <summary>
        /// Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext" />.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            IServiceProvider provider = context.HttpContext.RequestServices;
            ScopedDictionary dict = provider.GetService<ScopedDictionary>();
            IUnitOfWorkManager unitOfWorkManager = provider.GetService<IUnitOfWorkManager>();
            ILogger logger = provider.GetLogger<MvcUnitOfWorkFilter>();
            AjaxResultType type = AjaxResultType.Success;
            string message = null;
            if (context.Exception != null && !context.ExceptionHandled)
            {
                Exception ex = context.Exception;
                logger.LogError(new EventId(), ex, ex.Message);
                message = ex.Message;
                if (context.HttpContext.Request.IsAjaxRequest() || context.HttpContext.Request.IsJsonContextType())
                {
                    if (!context.HttpContext.Response.HasStarted)
                    {
                        context.Result = new JsonResult(new AjaxResult(ex.Message, AjaxResultType.Error));
                    }
                    context.ExceptionHandled = true;
                }
            }
            if (context.Result is JsonResult result1)
            {
                if (result1.Value is AjaxResult ajax)
                {
                    type = ajax.Type;
                    message = ajax.Content;
                    if (ajax.Succeeded())
                    {
                        unitOfWorkManager?.Commit();
                    }
                }
            }
            else if (context.Result is ObjectResult result2)
            {
                if (result2.Value is AjaxResult ajax)
                {
                    type = ajax.Type;
                    message = ajax.Content;
                    if (ajax.Succeeded())
                    {
                        unitOfWorkManager?.Commit();
                    }
                }
                else
                {
                    unitOfWorkManager?.Commit();
                }
            }
            //普通请求
            else if (context.HttpContext.Response.StatusCode >= 400)
            {
                switch (context.HttpContext.Response.StatusCode)
                {
                    case 401:
                        type = AjaxResultType.UnAuth;
                        break;

                    case 403:
                        type = AjaxResultType.Forbidden;
                        break;

                    case 404:
                        type = AjaxResultType.NoFound;
                        break;

                    case 423:
                        type = AjaxResultType.Locked;
                        break;

                    default:
                        type = AjaxResultType.Error;
                        break;
                }
            }
            else
            {
                type = AjaxResultType.Success;
                unitOfWorkManager?.Commit();
            }

            if (dict.AuditOperation != null)
            {
                dict.AuditOperation.ResultType = type;
                dict.AuditOperation.Message = message;
            }
        }
    }
}