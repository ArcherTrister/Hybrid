// -----------------------------------------------------------------------
//  <copyright file="IEmailSender.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

namespace ESoftor.Net
{
    /// <summary>
    /// 定义Email发送功能
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="email">接收人Email</param>
        /// <param name="subject">Email标题</param>
        /// <param name="message">Email内容</param>
        /// <returns></returns>
        Task SendEmailAsync(string email, string subject, string message);
    }
}