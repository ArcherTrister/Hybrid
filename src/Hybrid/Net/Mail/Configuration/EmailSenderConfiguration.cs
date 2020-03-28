using System.ComponentModel.DataAnnotations;

namespace Hybrid.Net.Mail.Configuration
{
    /// <summary>
    /// 邮件发送选项
    /// </summary>
    public sealed class EmailSenderConfiguration : IEmailSenderConfiguration
    {
        /// <summary>
        /// Creates a new <see cref="EmailSenderConfiguration"/>.
        /// </summary>
        public EmailSenderConfiguration()
        {
        }

        /// <summary>
        /// 获取或设置 邮件发送服务器
        /// </summary>
        [Required(ErrorMessage = "邮件发送服务器不能为空")]
        public string Host { get; set; }

        /// <summary>
        /// 获取或设置 发送方显示名
        /// </summary>
        [Required(ErrorMessage = "邮件发送方显示名称不能为空")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 获取或设置 发送方用户名
        /// </summary>
        [Required(ErrorMessage = "邮件发送用户名不能为空")]
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置 发送方密码
        /// </summary>
        [Required(ErrorMessage = "邮件发送密码不能为空")]
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置 邮件发送服务器端口
        /// </summary>
        [Required(ErrorMessage = "邮件发送服务器不能为空")]
        public int Port { get; set; } = 25;

        /// <summary>
        /// 登录SMTP服务器的域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 获取或设置 是否启用ssl
        /// </summary>
        public bool EnableSsl { get; set; } = true;

        /// <summary>
        /// 是否验证
        /// </summary>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get => true; set { } }

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