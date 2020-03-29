using System.Net.Mail;
using System.Threading.Tasks;


namespace Hybrid.Net
{
    /// <summary>
    /// 定义Email发送功能
    /// </summary>
    public interface IEmailSender
    {
        ///// <summary>
        ///// 发送Email
        ///// </summary>
        ///// <param name="email">接收人Email</param>
        ///// <param name="subject">Email标题</param>
        ///// <param name="message">Email内容</param>
        ///// <returns></returns>
        //Task SendEmailAsync(string email, string subject, string message);

        /// <summary>
        /// 异步发送Email
        /// </summary>
        /// <param name="to">接收人Email</param>
        /// <param name="subject">Email标题</param>
        /// <param name="body">Email内容</param>
        /// <param name="isBodyHtml">Email内容是否是Html</param>
        /// <returns></returns>
        Task SendAsync(string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="to">接收人Email</param>
        /// <param name="subject">Email标题</param>
        /// <param name="body">Email内容</param>
        /// <param name="isBodyHtml">Email内容是否是Html</param>
        /// <returns></returns>
        void Send(string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// 异步发送Email
        /// </summary>
        /// <param name="from">发送人Email</param>
        /// <param name="to">接收人Email</param>
        /// <param name="subject">Email标题</param>
        /// <param name="body">Email内容</param>
        /// <param name="isBodyHtml">Email内容是否是Html</param>
        /// <returns></returns>
        Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="from">发送人Email</param>
        /// <param name="to">接收人Email</param>
        /// <param name="subject">Email标题</param>
        /// <param name="body">Email内容</param>
        /// <param name="isBodyHtml">Email内容是否是Html</param>
        /// <returns></returns>
        void Send(string from, string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="mail">要发送的邮件</param>
        /// <param name="normalize">
        /// 是否要标准化邮件
        /// 如果是，如果他们没有设置地址/名称，他将自动设置，并且设置UTF-8编码
        /// </param>
        void Send(MailMessage mail, bool normalize = true);

        /// <summary>
        /// 异步发送Email
        /// </summary>
        /// <param name="mail">要发送的邮件</param>
        /// <param name="normalize">
        /// 是否要标准化邮件
        /// 如果是，如果他们没有设置地址/名称，他将自动设置，并且设置UTF-8编码
        /// </param>
        Task SendAsync(MailMessage mail, bool normalize = true);
    }
}
