using Hybrid.Zero.IdentityServer4;

namespace FlyingFish.Mobile.Ruqi.Dtos
{
    /// <summary>
    /// 登录返回模型
    /// </summary>
    public sealed class LoginResponse
    {
        /// <summary>
        /// Token
        /// </summary>
        public CustomTokenResponse TokenResponse { get; set; }

        /// <summary>
        /// UserInfo
        /// </summary>
        public CustomUserInfoResponse UserInfoResponse { get; set; }
    }
}