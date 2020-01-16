// -----------------------------------------------------------------------
//  <copyright file="IEmailSender.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

using System.Net.Mail;
using System.Threading.Tasks;

namespace Hybrid.Net.Mail
{
    /// <summary>
    /// 定义Email发送功能
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email.
        /// </summary>
        Task SendAsync(string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// Sends an email.
        /// </summary>
        void Send(string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// Sends an email.
        /// </summary>
        Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// Sends an email.
        /// </summary>
        void Send(string from, string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="mail">Mail to be sent</param>
        /// <param name="normalize">
        /// Should normalize email?
        /// If true, it sets sender address/name if it's not set before and makes mail encoding UTF-8. 
        /// </param>
        void Send(MailMessage mail, bool normalize = true);

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="mail">Mail to be sent</param>
        /// <param name="normalize">
        /// Should normalize email?
        /// If true, it sets sender address/name if it's not set before and makes mail encoding UTF-8. 
        /// </param>
        Task SendAsync(MailMessage mail, bool normalize = true);
    }
}