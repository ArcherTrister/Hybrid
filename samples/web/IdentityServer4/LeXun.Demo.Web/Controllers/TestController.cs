// -----------------------------------------------------------------------
//  <copyright file="TestController.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:50</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore;
using Hybrid.AspNetCore.Mvc.Filters;
using Hybrid.Collections;
using Hybrid.Data;

using LeXun.Demo.Identity;
using LeXun.Demo.Identity.Dtos;
using LeXun.Demo.Identity.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace LeXun.Demo.Web.Controllers
{
    [Description("网站-测试")]
    [ClassFilter]
    public class TestController : SiteApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly IIdentityContract _identityContract;

        public TestController(UserManager<User> userManager, IIdentityContract identityContract)
        {
            _userManager = userManager;
            _identityContract = identityContract;
        }

        [HttpGet]
        [UnitOfWork]
        [MethodFilter]
        [Description("测试01")]
        public async Task<string> Test01()
        {
            List<object> list = new List<object>();

            if (!_userManager.Users.Any())
            {
                RegisterDto dto = new RegisterDto
                {
                    UserName = "admin",
                    Password = "hybrid123456",
                    ConfirmPassword = "hybrid123456",
                    Email = "i66soft@qq.com",
                    NickName = "大站长",
                    RegisterIp = HttpContext.GetClientIp()
                };

                OperationResult<User> result = await _identityContract.Register(dto);
                if (result.Succeeded)
                {
                    User user = result.Data;
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                }
                list.Add(result.Message);

                dto = new RegisterDto()
                {
                    UserName = "hybrid",
                    Password = "hybrid123456",
                    Email = "mf.guo@qq.com",
                    NickName = "测试号",
                    RegisterIp = HttpContext.GetClientIp()
                };
                result = await _identityContract.Register(dto);
                if (result.Succeeded)
                {
                    User user = result.Data;
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                }
                list.Add(result.Message);
            }

            return list.ExpandAndToString("\r\n");
        }

        [HttpGet]
        public void Test02(TY ty)
        {
            int A = (int)ty;
        }

        public enum TY
        { 
        H,L,C
        }
    }

    public class ClassFilter : ActionFilterAttribute, IExceptionFilter
    {
        private ILogger _logger;

        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger = context.HttpContext.RequestServices.GetLogger<ClassFilter>();
            _logger.LogInformation("ClassFilter - OnActionExecuting");
        }

        /// <inheritdoc />
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("ClassFilter - OnActionExecuted");
        }

        /// <inheritdoc />
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation("ClassFilter - OnResultExecuting");
        }

        /// <inheritdoc />
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("ClassFilter - OnResultExecuted");
        }

        /// <summary>
        /// Called after an action has thrown an <see cref="T:System.Exception" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception.Message;
            _logger.LogInformation("ClassFilter - OnException");
        }
    }

    public class MethodFilter : ActionFilterAttribute
    {
        private ILogger _logger;

        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger = context.HttpContext.RequestServices.GetLogger<MethodFilter>();
            _logger.LogInformation("MethodFilter - OnActionExecuting");
        }

        /// <inheritdoc />
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("MethodFilter - OnActionExecuted");
        }

        /// <inheritdoc />
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation("MethodFilter - OnResultExecuting");
        }

        /// <inheritdoc />
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("MethodFilter - OnResultExecuted");
        }
    }
}