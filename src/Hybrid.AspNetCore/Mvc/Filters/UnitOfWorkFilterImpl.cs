﻿// -----------------------------------------------------------------------
//  <copyright file="UnitOfWorkFilterImpl.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2019 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2019-05-14 17:29</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Extensions;
using Hybrid.AspNetCore.UI;
using Hybrid.Data;
using Hybrid.Dependency;
using Hybrid.Domain.Uow;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;

namespace Hybrid.AspNetCore.Mvc.Filters
{
    internal class UnitOfWorkFilterImpl : IActionFilter
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ILogger _logger;

        /// <summary>
        /// 初始化一个<see cref="UnitOfWorkFilterImpl"/>类型的新实例
        /// </summary>
        public UnitOfWorkFilterImpl(IServiceProvider serviceProvider)
        {
            _unitOfWorkManager = serviceProvider.GetService<IUnitOfWorkManager>();
            _logger = serviceProvider.GetLogger<UnitOfWorkFilterImpl>();
        }

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
            ScopedDictionary dict = context.HttpContext.RequestServices.GetService<ScopedDictionary>();
            AjaxResultType type = AjaxResultType.Success;
            string message = null;
            if (context.Exception != null && !context.ExceptionHandled)
            {
                Exception ex = context.Exception;
                _logger.LogError(new EventId(), ex, ex.Message);
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
                        _unitOfWorkManager?.Commit();
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
                        _unitOfWorkManager?.Commit();
                    }
                }
                else
                {
                    _unitOfWorkManager?.Commit();
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
                        type = AjaxResultType.UnAuth;
                        break;
                    case 404:
                        type = AjaxResultType.UnAuth;
                        break;
                    case 423:
                        type = AjaxResultType.UnAuth;
                        break;
                    default:
                        type = AjaxResultType.Error;
                        break;
                }
            }
            else
            {
                type = AjaxResultType.Success;
                _unitOfWorkManager?.Commit();
            }

            if (dict.AuditOperation != null)
            {
                dict.AuditOperation.ResultType = type;
                dict.AuditOperation.Message = message;
            }
        }
    }
}