// -----------------------------------------------------------------------
//  <copyright file="AuditActionFilter" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-12 15:11:41</last-date>
// -----------------------------------------------------------------------

using Hybrid.Aspects;
using Hybrid.AspNetCore.Extensions;
using Hybrid.AspNetCore.UI;
using Hybrid.Audits;
using Hybrid.Audits.Configuration;
using Hybrid.Core.Functions;
using Hybrid.Extensions;
using Hybrid.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hybrid.AspNetCore.Mvc.Filters
{
    public class AuditActionFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger<AuditActionFilter> _logger;

        public AuditActionFilter(IServiceProvider provider, ILoggerFactory loggerFactory)
        {
            _provider = provider;
            _logger = loggerFactory.CreateLogger<AuditActionFilter>();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IAuditingConfiguration configuration = _provider.GetRequiredService<IAuditingConfiguration>();
            if (!ShouldSaveAudit(context, configuration))
            {
                await next();
                return;
            }

            using (CrossCuttingConcerns.Applying(context.Controller, CrossCuttingConcerns.Auditing))
            {
                IAuditStore store = _provider.GetService<IAuditStore>();
                IFunction function = context.GetExecuteFunction();
                //var auditInfo = store.CreateAuditInfo(
                //    context.ActionDescriptor.AsControllerActionDescriptor().ControllerTypeInfo.AsType(),
                //    context.ActionDescriptor.AsControllerActionDescriptor().MethodInfo,
                //    context.ActionArguments
                //);
                Type type = context.ActionDescriptor.AsControllerActionDescriptor().ControllerTypeInfo.AsType();
                List<Type> ignoredTypes = configuration.IgnoredTypes;
                AuditOperationEntry operation = new AuditOperationEntry
                {
                    FunctionName = function.Name,
                    ClientIpAddress = context.HttpContext.GetClientIp(),
                    UserAgent = context.HttpContext.Request.Headers["User-Agent"].FirstOrDefault(),
                    CreatedTime = DateTime.Now,
                    ServiceName = type != null
                    ? type.FullName
                    : "",
                    Parameters = ConvertArgumentsToJson(context.ActionArguments, ignoredTypes),
                };
                if (context.HttpContext.User.Identity.IsAuthenticated && context.HttpContext.User.Identity is ClaimsIdentity identity)
                {
                    operation.UserId = identity.GetUserId();
                    operation.UserName = identity.GetUserName();
                }

                var stopwatch = Stopwatch.StartNew();

                ActionExecutedContext result = null;
                try
                {
                    result = await next();
                    if (result.Exception != null && !result.ExceptionHandled)
                    {
                        operation.Exception = result.Exception;
                    }
                }
                catch (Exception ex)
                {
                    operation.Exception = ex;
                    throw;
                }
                finally
                {
                    stopwatch.Stop();
                    operation.Elapsed = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);

                    if (configuration.SaveReturnValues && result != null)
                    {
                        switch (result.Result)
                        {
                            case ObjectResult objectResult:
                                operation.ReturnValue = AuditingHelper.Serialize(objectResult.Value, ignoredTypes);
                                break;

                            case JsonResult jsonResult:
                                operation.ReturnValue = AuditingHelper.Serialize(jsonResult.Value, ignoredTypes);
                                break;

                            case ContentResult contentResult:
                                operation.ReturnValue = contentResult.Content;
                                break;

                            case AjaxResult ajaxResult:
                                operation.ReturnValue = ajaxResult.Content;
                                break;
                        }
                    }

                    await store.SaveAsync(operation);
                }
            }
        }

        private string ConvertArgumentsToJson(IDictionary<string, object> arguments, List<Type> ignoredTypes)
        {
            try
            {
                if (arguments.IsNullOrEmpty())
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();

                foreach (var argument in arguments)
                {
                    if (argument.Value != null && ignoredTypes.Any(t => t.IsInstanceOfType(argument.Value)))
                    {
                        dictionary[argument.Key] = null;
                    }
                    else
                    {
                        dictionary[argument.Key] = argument.Value;
                    }
                }

                return AuditingHelper.Serialize(dictionary, ignoredTypes);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString(), ex);
                return "{}";
            }
        }

        private bool ShouldSaveAudit(ActionExecutingContext actionContext, IAuditingConfiguration configuration)
        {
            return AuditingHelper.ShouldSaveAudit(configuration, actionContext.HttpContext.User, actionContext.GetExecuteFunction(), actionContext.ActionDescriptor.GetMethodInfo());
        }
    }
}