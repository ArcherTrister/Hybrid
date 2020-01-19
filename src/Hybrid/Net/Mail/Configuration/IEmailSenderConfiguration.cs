using Hybrid.Domain.Entities;

namespace Hybrid.Net.Mail.Configuration
{
    public interface IEmailSenderConfiguration: IEnabled
    {
        /// <summary>
        /// 获取或设置 邮件发送服务器
        /// </summary>
        string Host { get; set; }

        /// <summary>
        /// 获取或设置 发送方显示名
        /// </summary>
        string DisplayName { get; set; }

        /// <summary>
        /// 获取或设置 发送方用户名
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// 获取或设置 发送方密码
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// 获取或设置 邮件发送服务器端口
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// 登录SMTP服务器的域名
        /// </summary>
        string Domain { get; set; }

        /// <summary>
        /// 获取或设置 是否启用ssl
        /// </summary>
        bool EnableSsl { get; set; }

        /// <summary>
        /// 是否验证
        /// </summary>
        bool UseDefaultCredentials { get; set; }

        ///// <summary>
        ///// 安全套接字选项
        ///// </summary>
        //public SecureSocketOptions? SecureSocketOption { get; set; }

        ///// <summary>
        ///// 获取或设置 邮件发送超时时间
        ///// </summary>
        //public int Timeout { get; set; } = 9999;

        //public string TargetName { get; set; }

        //public ServicePoint ServicePoint { get; }

        //public string PickupDirectoryLocation { get; set; }

        //public SmtpDeliveryMethod DeliveryMethod { get; set; }

        //public SmtpDeliveryFormat DeliveryFormat { get; set; }

        //public ICredentialsByHost Credentials { get; set; }
    }
}
