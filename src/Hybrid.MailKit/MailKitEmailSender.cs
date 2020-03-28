using Hybrid.Exceptions;
using Hybrid.Net.Mail;
using Hybrid.Net.Mail.Configuration;

using MailKit.Security;

using MimeKit;

using System;
using System.Net.Mail;
using System.Threading.Tasks;

using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Hybrid.MailKit
{
    /// <summary>
    /// 通过SMTP发送电子邮件
    /// </summary>
    public class MailKitEmailSender : EmailSenderBase
    {
        private readonly IEmailSenderConfiguration _configuration;

        /// <summary>
        /// 创建一个新的MailKitEmailSender实例 <see cref="MailKitEmailSender"/>.
        /// </summary>
        /// <param name="provider">provider</param>
        public MailKitEmailSender(IEmailSenderConfiguration configuration)
    : base(configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 创建发送邮件客户端
        /// </summary>
        /// <returns></returns>
        public virtual SmtpClient BuildMailKitClient()
        {
            var client = new SmtpClient();

            try
            {
                ConfigureClient(client);
                return client;
            }
            catch (Exception)
            {
                client.Dispose();
                throw new HybridException("创建发送邮件客户端失败");
            }
        }

        protected virtual void ConfigureClient(SmtpClient client)
        {
            client.Connect(
                _configuration.Host,
                _configuration.Port,
                GetSecureSocketOption()
            );

            if (_configuration.UseDefaultCredentials)
            {
                return;
            }

            client.Authenticate(
                _configuration.UserName,
                _configuration.Password
            );
        }

        protected virtual SecureSocketOptions GetSecureSocketOption()
        {
            //if (mailSenderOptions.SecureSocketOption.HasValue)
            //{
            //    return mailSenderOptions.SecureSocketOption.Value;
            //}

            return _configuration.EnableSsl
                ? SecureSocketOptions.SslOnConnect
                : SecureSocketOptions.None;
        }

        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="mail">要发送的邮件</param>
        /// <returns></returns>
        protected override async Task SendEmailAsync(MailMessage mail)
        {
            using (var client = BuildMailKitClient())
            {
                var message = mail.ToMimeMessage();
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        /// <summary>
        /// 同步发送邮件
        /// </summary>
        /// <param name="mail">要发送的邮件</param>
        protected override void SendEmail(MailMessage mail)
        {
            using (var client = BuildMailKitClient())
            {
                var message = mail.ToMimeMessage();
                client.Send(message);
                client.Disconnect(true);
            }
        }

        ///// <summary>
        ///// 发送Email
        ///// </summary>
        ///// <param name="from">发送人Email</param>
        ///// <param name="to">接收人Email</param>
        ///// <param name="subject">Email标题</param>
        ///// <param name="body">Email内容</param>
        ///// <param name="isBodyHtml">Email内容是否是Html</param>
        ///// <returns></returns>
        //public override void SendEmail(string from, string to, string subject, string body, bool isBodyHtml = true)
        //{
        //    using (var client = BuildMailKitClient())
        //    {
        //        var message = BuildMimeMessage(from, to, subject, body, isBodyHtml);
        //        client.Send(message);
        //        client.Disconnect(true);
        //    }
        //}

        ///// <summary>
        ///// 发送Email
        ///// </summary>
        ///// <param name="from">发送人Email</param>
        ///// <param name="to">接收人Email</param>
        ///// <param name="subject">Email标题</param>
        ///// <param name="body">Email内容</param>
        ///// <param name="isBodyHtml">Email内容是否是Html</param>
        ///// <returns></returns>
        //public override async Task SendEmailAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        //{
        //    using (var client = BuildMailKitClient())
        //    {
        //        var message = BuildMimeMessage(from, to, subject, body, isBodyHtml);
        //        await client.SendAsync(message);
        //        await client.DisconnectAsync(true);
        //    }
        //}

        private static MimeMessage BuildMimeMessage(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            var bodyType = isBodyHtml ? "html" : "plain";
            var message = new MimeMessage
            {
                Subject = subject,
                Body = new TextPart(bodyType)
                {
                    Text = body
                }
            };

            message.From.Add(new MailboxAddress(from));
            message.To.Add(new MailboxAddress(to));

            return message;
        }
    }
}