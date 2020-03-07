// -----------------------------------------------------------------------
//  <copyright file="OperateAuditFilter.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Extensions;
using Hybrid.AspNetCore.UI;
using Hybrid.Audits;
using Hybrid.Audits.Configuration;
using Hybrid.Authorization.Functions;
using Hybrid.Dependency;
using Hybrid.Domain.Uow;
using Hybrid.Authorization;
using Hybrid.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Linq;
using System.Security.Claims;

namespace Hybrid.AspNetCore.Mvc.Filters
{
    public class OperateAuditFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            IServiceProvider provider = context.HttpContext.RequestServices;
            IFunction function = context.GetExecuteFunction();
            if (function == null)
            {
                return;
            }
            ScopedDictionary dict = provider.GetService<ScopedDictionary>();
            dict.Function = function;
            // 数据权限有效角色，即有当前功能权限的角色
            IFunctionAuthorization functionAuthorization = provider.GetService<IFunctionAuthorization>();
            ClaimsPrincipal principal = context.HttpContext.User;
            string[] roleName = functionAuthorization.GetOkRoles(function, principal);
            dict.DataAuthValidRoleNames = roleName;
            IAuditingConfiguration configuration = provider.GetRequiredService<IAuditingConfiguration>();
            if (!AuditingHelper.ShouldSaveAudit(configuration, principal, function, context.ActionDescriptor.GetMethodInfo()))
            {
                return;
            }
            AuditOperationEntry operation = new AuditOperationEntry
            {
                FunctionName = function.Name,
                ClientIpAddress = context.HttpContext.GetClientIp(),
                UserAgent = context.HttpContext.Request.Headers["User-Agent"].FirstOrDefault(),
                CreatedTime = DateTime.Now
            };
            if (principal.Identity.IsAuthenticated && principal.Identity is ClaimsIdentity identity)
            {
                operation.UserId = identity.GetUserId();
                operation.UserName = identity.GetUserName();
            }

            dict.AuditOperation = operation;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            IServiceProvider provider = context.HttpContext.RequestServices;
            ScopedDictionary dict = provider.GetService<ScopedDictionary>();
            if (dict.AuditOperation?.FunctionName == null)
            {
                return;
            }
            dict.AuditOperation.EndedTime = DateTime.Now;
            //IUnitOfWork unitOfWork = provider.GetUnitOfWork<Function, Guid>();
            ////回滚之前业务处理中的未提交事务，防止审计信息保存时误提交
            //unitOfWork?.Rollback();
            IUnitOfWorkManager unitOfWorkManager = provider.GetService<IUnitOfWorkManager>();
            unitOfWorkManager?.Rollback();
            IAuditStore store = provider.GetService<IAuditStore>();
            store?.Save(dict.AuditOperation);
            unitOfWorkManager?.Commit();
        }
    }

    //public class AsyncOperateAuditFilter : IAsyncActionFilter
    //{
    //    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    //    {
    //        IServiceProvider provider = context.HttpContext.RequestServices;
    //        IFunction function = context.GetExecuteFunction();
    //        if (function == null)
    //        {
    //            await next();
    //            return;
    //        }
    //        ScopedDictionary dict = provider.GetService<ScopedDictionary>();
    //        dict.Function = function;
    //        // 数据权限有效角色，即有当前功能权限的角色
    //        IFunctionAuthorization functionAuthorization = provider.GetService<IFunctionAuthorization>();
    //        string[] roleName = functionAuthorization.GetOkRoles(function, context.HttpContext.User);
    //        dict.DataAuthValidRoleNames = roleName;
    //        IIdentity principal = context.HttpContext.User.Identity;
    //        if (AuditingHelper.ShouldSaveAudit(provider, principal, context.ActionDescriptor.GetMethodInfo(), true))
    //        {
    //            await next();
    //            return;
    //        }
    //        if (!function.AuditOperationEnabled)
    //        {
    //            await next();
    //            return;
    //        }

    //        using (Aspects.CrossCuttingConcerns.Applying(context.Controller, Aspects.CrossCuttingConcerns.Auditing))
    //        {
    //            AuditOperationEntry operation = new AuditOperationEntry
    //            {
    //                FunctionName = function.Name,
    //                Ip = context.HttpContext.GetClientIp(),
    //                UserAgent = context.HttpContext.Request.Headers["User-Agent"].FirstOrDefault(),
    //                CreatedTime = DateTime.Now
    //            };
    //            if (principal.IsAuthenticated && principal is ClaimsIdentity identity)
    //            {
    //                operation.UserId = identity.GetUserId();
    //                operation.UserName = identity.GetUserName();
    //                operation.NickName = identity.GetNickName();
    //            }

    //            dict.AuditOperation = operation;

    //            ActionExecutedContext result = null;
    //            try
    //            {
    //                IUnitOfWorkManager unitOfWorkManager = provider.GetService<IUnitOfWorkManager>();
    //                ILogger logger = provider.GetLogger<MvcUnitOfWorkFilter>();
    //                AjaxResultType type = AjaxResultType.Success;
    //                string message = null;
    //                if (context.Exception != null && !context.ExceptionHandled)
    //                {
    //                    Exception ex = context.Exception;
    //                    logger.LogError(new EventId(), ex, ex.Message);
    //                    message = ex.Message;
    //                    if (context.HttpContext.Request.IsAjaxRequest() || context.HttpContext.Request.IsJsonContextType())
    //                    {
    //                        if (!context.HttpContext.Response.HasStarted)
    //                        {
    //                            context.Result = new JsonResult(new AjaxResult(ex.Message, AjaxResultType.Error));
    //                        }
    //                        context.ExceptionHandled = true;
    //                    }
    //                }
    //                if (context.Result is JsonResult result1)
    //                {
    //                    if (result1.Value is AjaxResult ajax)
    //                    {
    //                        type = ajax.Type;
    //                        message = ajax.Content;
    //                        if (ajax.Succeeded())
    //                        {
    //                            unitOfWorkManager?.Commit();
    //                        }
    //                    }
    //                }
    //                else if (context.Result is ObjectResult result2)
    //                {
    //                    if (result2.Value is AjaxResult ajax)
    //                    {
    //                        type = ajax.Type;
    //                        message = ajax.Content;
    //                        if (ajax.Succeeded())
    //                        {
    //                            unitOfWorkManager?.Commit();
    //                        }
    //                    }
    //                    else
    //                    {
    //                        unitOfWorkManager?.Commit();
    //                    }
    //                }
    //                //普通请求
    //                else if (context.HttpContext.Response.StatusCode >= 400)
    //                {
    //                    switch (context.HttpContext.Response.StatusCode)
    //                    {
    //                        case 401:
    //                            type = AjaxResultType.UnAuth;
    //                            break;
    //                        case 403:
    //                            type = AjaxResultType.Forbidden;
    //                            break;
    //                        case 404:
    //                            type = AjaxResultType.NoFound;
    //                            break;
    //                        case 423:
    //                            type = AjaxResultType.Locked;
    //                            break;
    //                        default:
    //                            type = AjaxResultType.Error;
    //                            break;
    //                    }
    //                }
    //                else
    //                {
    //                    type = AjaxResultType.Success;
    //                    unitOfWorkManager?.Commit();
    //                }

    //                if (dict.AuditOperation != null)
    //                {
    //                    dict.AuditOperation.ResultType = type;
    //                    dict.AuditOperation.Message = message;
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                auditInfo.Exception = ex;
    //                throw;
    //            }
    //            finally
    //            {
    //                if (!dict.AuditOperation?.FunctionName.IsEmpty())
    //                {
    //                    dict.AuditOperation.EndedTime = DateTime.Now;
    //                    //IUnitOfWork unitOfWork = provider.GetUnitOfWork<Function, Guid>();
    //                    ////回滚之前业务处理中的未提交事务，防止审计信息保存时误提交
    //                    //unitOfWork?.Rollback();
    //                    IUnitOfWorkManager unitOfWorkManager = provider.GetService<IUnitOfWorkManager>();
    //                    unitOfWorkManager?.Rollback();
    //                    IAuditStore store = provider.GetService<IAuditStore>();
    //                    store?.Save(dict.AuditOperation);
    //                    unitOfWorkManager?.Commit();
    //                }

    //            }
    //        }

    //    }
    //}
}