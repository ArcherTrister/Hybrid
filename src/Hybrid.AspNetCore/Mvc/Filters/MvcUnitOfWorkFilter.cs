﻿// -----------------------------------------------------------------------
//  <copyright file="MvcUnitOfWorkFilter.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
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
    internal class MvcUnitOfWorkFilter : IActionFilter
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ILogger _logger;

        /// <summary>
        /// 初始化一个<see cref="MvcUnitOfWorkFilter"/>类型的新实例
        /// </summary>
        public MvcUnitOfWorkFilter(IServiceProvider serviceProvider)
        {
            _unitOfWorkManager = serviceProvider.GetService<IUnitOfWorkManager>();
            _logger = serviceProvider.GetLogger<MvcUnitOfWorkFilter>();
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
            if (context.Result is JsonResult jsonResult)
            {
                if (jsonResult.Value is AjaxResult ajax)
                {
                    type = ajax.ResultType;
                    message = ajax.Content;
                    if (ajax.Success)
                    {
                        _unitOfWorkManager?.Commit();
                    }
                }
            }
            else if (context.Result is ObjectResult objectResult)
            {
                if (objectResult.Value is AjaxResult ajax)
                {
                    type = ajax.ResultType;
                    message = ajax.Content;
                    if (ajax.Success)
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
                    case 400:
                        type = AjaxResultType.RequestError;
                        break;

                    case 401:
                        type = AjaxResultType.UnAuth;
                        break;

                    case 403:
                        type = AjaxResultType.Forbidden;
                        break;

                    case 404:
                        type = AjaxResultType.NoFound;
                        break;

                    case 405:
                        type = AjaxResultType.MethodDisabled;
                        break;

                    case 406:
                        type = AjaxResultType.NoSupport;
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