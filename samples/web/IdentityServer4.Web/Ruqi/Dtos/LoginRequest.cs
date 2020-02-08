using System.ComponentModel.DataAnnotations;

namespace FlyingFish.Mobile.Ruqi.Dtos
{
    /// <summary>
    /// 登录请求模型
    /// </summary>
    public sealed class LoginRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}