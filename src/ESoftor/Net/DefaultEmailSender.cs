// -----------------------------------------------------------------------
//  <copyright file="DefaultEmailSender.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-05-08 2:47</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Options;
using ESoftor.Dependency;
using ESoftor.Exceptions;
using ESoftor.Extensions;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ESoftor.Net
{
    /// <summary>
    /// 默认邮件发送者
    /// </summary>
    [Dependency(ServiceLifetime.Singleton, TryAdd = true)]
    public class DefaultEmailSender : IEmailSender
    {
        private readonly IServiceProvider _provider;

        /// <summary>
        /// 初始化一个<see cref="DefaultEmailSender"/>类型的新实例
        /// </summary>
        public DefaultEmailSender(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="email">接收人Email</param>
        /// <param name="subject">Email标题</param>
        /// <param name="body">Email内容</param>
        /// <returns></returns>
        public Task SendEmailAsync(string email, string subject, string body)
        {
            ESoftorOptions options = _provider.GetESoftorOptions();
            MailSenderOptions mailSender = options.MailSender;
            if (mailSender == null || mailSender.Host == null || mailSender.Host.Contains("请替换"))
            {
                throw new ESoftorException("邮件发送选项不存在，请在appsetting.json配置ESoftor.MailSender节点");
            }

            string host = mailSender.Host,
                displayName = mailSender.DisplayName,
                userName = mailSender.UserName,
                password = mailSender.Password;
            SmtpClient client = new SmtpClient(host)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(userName, password)
            };

            string fromEmail = userName.Contains("@") ? userName : "{0}@{1}".FormatWith(userName, client.Host.Replace("smtp.", ""));
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(fromEmail, displayName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mail.To.Add(email);
            return client.SendMailAsync(mail);
        }
    }
}