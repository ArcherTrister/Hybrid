using System.ComponentModel.DataAnnotations;

namespace IdentityServer4.Web.Ruqi.Dtos
{
    /// <summary>
    /// 退出请求模型
    /// </summary>
    public sealed class LogoutRequest
    {
        /// <summary>
        /// 刷新Token
        /// </summary>
        [Required]
        public string RefreshToken { get; set; }
    }
}