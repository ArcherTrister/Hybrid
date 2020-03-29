// -----------------------------------------------------------------------
//  <copyright file="DefaultEmailSender.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-05-08 2:47</last-date>
// -----------------------------------------------------------------------

using Hybrid.Extensions;
using Hybrid.Net.Mail;
using Hybrid.Net.Mail.Configuration;
using Hybrid.Net.Mail.Smtp;

using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Hybrid.Net
{
    /// <summary>
    /// 默认邮件发送者
    /// </summary>
    public class DefaultEmailSender : EmailSenderBase, ISmtpEmailSender
    {
        private readonly IEmailSenderConfiguration _configuration;

        /// <summary>
        /// Creates a new <see cref="SmtpEmailSender"/>.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public DefaultEmailSender(IEmailSenderConfiguration configuration)
            : base(configuration)
        {
            _configuration = configuration;
        }

        public SmtpClient BuildClient()
        {
            var host = _configuration.Host;
            var port = _configuration.Port;

            var smtpClient = new SmtpClient(host, port);
            try
            {
                if (_configuration.EnableSsl)
                {
                    smtpClient.EnableSsl = true;
                }

                if (_configuration.UseDefaultCredentials)
                {
                    smtpClient.UseDefaultCredentials = true;
                }
                else
                {
                    smtpClient.UseDefaultCredentials = false;

                    var userName = _configuration.UserName;
                    if (!userName.IsNullOrEmpty())
                    {
                        var password = _configuration.Password;
                        var domain = _configuration.Domain;
                        smtpClient.Credentials = !domain.IsNullOrEmpty()
                            ? new NetworkCredential(userName, password, domain)
                            : new NetworkCredential(userName, password);
                    }
                }

                return smtpClient;
            }
            catch
            {
                smtpClient.Dispose();
                throw;
            }
        }

        protected override async Task SendEmailAsync(MailMessage mail)
        {
            using (var smtpClient = BuildClient())
            {
                await smtpClient.SendMailAsync(mail);
            }
        }

        protected override void SendEmail(MailMessage mail)
        {
            using (var smtpClient = BuildClient())
            {
                smtpClient.Send(mail);
            }
        }

        //private readonly IServiceProvider _provider;

        ///// <summary>
        ///// 初始化一个<see cref="DefaultEmailSender"/>类型的新实例
        ///// </summary>
        //public DefaultEmailSender(IServiceProvider provider)
        //{
        //    _provider = provider;
        //}

        ///// <summary>
        ///// 发送Email
        ///// </summary>
        ///// <param name="email">接收人Email</param>
        ///// <param name="subject">Email标题</param>
        ///// <param name="body">Email内容</param>
        ///// <returns></returns>
        //public async Task SendEmailAsync(string email, string subject, string body)
        //{
        //    HybridOptions options = _provider.GetHybridOptions();
        //    MailSenderOptions mailSender = options.MailSender;
        //    if (mailSender == null || mailSender.Host == null || mailSender.Host.Contains("请替换"))
        //    {
        //        throw new HybridException("邮件发送选项不存在，请在appsetting.json配置Hybrid:MailSender节点");
        //    }

        //    string host = mailSender.Host,
        //        displayName = mailSender.DisplayName,
        //        userName = mailSender.UserName,
        //        password = mailSender.Password;
        //    bool enableSsl = mailSender.EnableSsl;
        //    int port = mailSender.Port;
        //    if (port == 0)
        //    {
        //        port = enableSsl ? 465 : 25;
        //    }

        //    SmtpClient client = new SmtpClient(host, port)
        //    {
        //        UseDefaultCredentials = true,
        //        EnableSsl = enableSsl,
        //        Credentials = new NetworkCredential(userName, password)
        //    };

        //    string fromEmail = userName.Contains("@") ? userName : "{0}@{1}".FormatWith(userName, client.Host.Replace("smtp.", ""));
        //    MailMessage mail = new MailMessage
        //    {
        //        From = new MailAddress(fromEmail, displayName),
        //        Subject = subject,
        //        Body = body,
        //        IsBodyHtml = true
        //    };
        //    mail.To.Add(email);
        //    await client.SendMailAsync(mail);
        //}
    }
}