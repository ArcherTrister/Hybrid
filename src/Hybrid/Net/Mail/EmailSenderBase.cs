using Hybrid.Core.Options;
using Hybrid.Exceptions;
using Hybrid.Extensions;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hybrid.Net.Mail
{
    /// <summary>
    /// 默认邮件发送者
    /// </summary>
    public abstract class EmailSenderBase : IEmailSender
    {
        private readonly IServiceProvider _provider;

        /// <summary>
        /// SMTP服务器配置参数
        /// </summary>
        public MailSenderConfiguration MailSenderOptions { get; }

        /// <summary>
        /// 初始化一个<see cref="EmailSenderBase"/>类型的新实例
        /// </summary>
        protected EmailSenderBase(IServiceProvider provider)
        {
            _provider = provider;
            MailSenderOptions = _provider.GetHybridOptions().MailSenderConfiguration;
            if (MailSenderOptions == null ||
                MailSenderOptions.Host.IsNullOrEmpty() ||
                MailSenderOptions.Host.Contains("请替换") ||
                MailSenderOptions.UserName.IsNullOrEmpty() ||
                MailSenderOptions.Password.IsNullOrEmpty())
            {
                throw new HybridException("邮件发送选项不存在，请在appsetting.json配置Hybrid.MailKitSender节点");
            }
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="from">发送人Email</param>
        /// <param name="to">接收人Email</param>
        /// <param name="subject">Email标题</param>
        /// <param name="body">Email内容</param>
        /// <param name="isBodyHtml">Email内容是否是Html</param>
        /// <returns></returns>
        public virtual async Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendAsync(new MailMessage
            {
                To = { to },
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml
            });
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="from">发送人Email</param>
        /// <param name="to">接收人Email</param>
        /// <param name="subject">Email标题</param>
        /// <param name="body">Email内容</param>
        /// <param name="isBodyHtml">Email内容是否是Html</param>
        /// <returns></returns>
        public virtual void Send(string to, string subject, string body, bool isBodyHtml = true)
        {
            Send(new MailMessage
            {
                To = { to },
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml
            });
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="from">发送人Email</param>
        /// <param name="to">接收人Email</param>
        /// <param name="subject">Email标题</param>
        /// <param name="body">Email内容</param>
        /// <param name="isBodyHtml">Email内容是否是Html</param>
        /// <returns></returns>
        public virtual async Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendAsync(new MailMessage(from, to, subject, body) { IsBodyHtml = isBodyHtml });
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="from">发送人Email</param>
        /// <param name="to">接收人Email</param>
        /// <param name="subject">Email标题</param>
        /// <param name="body">Email内容</param>
        /// <param name="isBodyHtml">Email内容是否是Html</param>
        /// <returns></returns>
        public virtual void Send(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            Send(new MailMessage(from, to, subject, body) { IsBodyHtml = isBodyHtml });
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="mail">要发送的邮件</param>
        /// <param name="normalize">
        /// 是否要标准化邮件
        /// 如果是，如果他们没有设置地址/名称，他将自动设置，并且设置UTF-8编码
        /// </param>
        public virtual async Task SendAsync(MailMessage mail, bool normalize = true)
        {
            if (normalize)
            {
                NormalizeMail(mail);
            }

            await SendEmailAsync(mail);
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="mail">要发送的邮件</param>
        /// <param name="normalize">
        /// 是否要标准化邮件
        /// 如果是，如果他们没有设置地址/名称，他将自动设置，并且设置UTF-8编码
        /// </param>
        public virtual void Send(MailMessage mail, bool normalize = true)
        {
            if (normalize)
            {
                NormalizeMail(mail);
            }

            SendEmail(mail);
        }

        /// <summary>
        /// Should implement this method to send email in derived classes.
        /// </summary>
        /// <param name="mail">Mail to be sent</param>
        protected abstract Task SendEmailAsync(MailMessage mail);

        /// <summary>
        /// Should implement this method to send email in derived classes.
        /// </summary>
        /// <param name="mail">Mail to be sent</param>
        protected abstract void SendEmail(MailMessage mail);

        /// <summary>
        /// 标准化给定的电子邮件
        /// 如果之前没有赋值则进行默认赋值 <see cref="MailMessage.From"/>
        /// 如果未设置UTF8，则将编码设置为UTF8
        /// </summary>
        /// <param name="mail">要标准化的邮件</param>
        protected virtual void NormalizeMail(MailMessage mail)
        {
            if (mail.From == null || mail.From.Address.IsNullOrEmpty())
            {
                mail.From = new MailAddress(
                    MailSenderOptions.UserName,
                    MailSenderOptions.DisplayName,
                    Encoding.UTF8
                    );
            }

            if (mail.HeadersEncoding == null)
            {
                mail.HeadersEncoding = Encoding.UTF8;
            }

            if (mail.SubjectEncoding == null)
            {
                mail.SubjectEncoding = Encoding.UTF8;
            }

            if (mail.BodyEncoding == null)
            {
                mail.BodyEncoding = Encoding.UTF8;
            }
        }
    }
}