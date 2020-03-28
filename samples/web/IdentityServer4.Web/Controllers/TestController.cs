// -----------------------------------------------------------------------
//  <copyright file="TestController.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.Mvc.Controllers;
using Hybrid.Audits.Configuration;
using Hybrid.Localization.Configuration;
using Hybrid.Net.Mail;
using Hybrid.Net.Mail.Configuration;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hybrid.Web.Controllers
{
    [Description("网站-测试")]
    public class TestController : LocalApiController
    {
        private readonly IServiceProvider _serviceProvider;

        public TestController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            ILocalizationConfiguration LocalizationConfiguration = _serviceProvider.GetRequiredService<ILocalizationConfiguration>();
            IEmailSenderConfiguration EmailSenderConfiguration = _serviceProvider.GetRequiredService<IEmailSenderConfiguration>();
            IAuditingConfiguration AuditingConfiguration = _serviceProvider.GetRequiredService<IAuditingConfiguration>();
        }

        private IEmailSender EmailSender => _serviceProvider.GetRequiredService<IEmailSender>();

        //protected IEmailSender EmailSender => _serviceProvider.GetRequiredService<IEmailSender>();
        //protected ILocalizationConfiguration LocalizationConfiguration => _serviceProvider.GetRequiredService<ILocalizationConfiguration>();
        //protected IEmailSenderConfiguration EmailSenderConfiguration => _serviceProvider.GetRequiredService<IEmailSenderConfiguration>();
        //protected IAuditingConfiguration AuditingConfiguration => _serviceProvider.GetRequiredService<IAuditingConfiguration>();

        //protected IHybridStartupConfiguration HybridStartupConfiguration => _serviceProvider.GetRequiredService<IHybridStartupConfiguration>();

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public IActionResult Get()
        {
            return new JsonResult(
                from c in User.Claims select new { c.Type, c.Value });
        }

        /// <summary>
        /// 日志测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public void LoggerTest()
        {
            Logger.LogInformation("Hello Hybrid");
        }

        /// <summary>
        /// 发送邮件测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task EmailTest()
        {
            await EmailSender.SendAsync("319807406@qq.com", "Hello", "This is a test email!", true);
        }
    }
}