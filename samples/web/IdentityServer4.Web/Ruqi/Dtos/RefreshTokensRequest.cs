using System.ComponentModel.DataAnnotations;

namespace IdentityServer4.Web.Ruqi.Dtos
{
    /// <summary>
    /// 刷新Token请求模型
    /// </summary>
    public sealed class RefreshTokensRequest
    {
        /// <summary>
        /// 刷新Token
        /// </summary>
        [Required]
        public string RefreshToken { get; set; }
    }
}