using System.ComponentModel.DataAnnotations;

namespace IdentityServer4.Web.Ruqi.Dtos
{
    /// <summary>
    /// 注册请求模型
    /// </summary>
    public sealed class RegisterRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Compare("Password", ErrorMessage = "密码与确认密码不匹配")]
        [Required]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} 应在 {2}~{1} 个字符以内")]
        public string NickName { get; set; }

        /// <summary>
        /// 注册IP
        /// </summary>
        public string RegisterIp { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerifyCode { get; set; }

        /// <summary>
        /// 验证码编号
        /// </summary>
        public string VerifyCodeId { get; set; }
    }
}