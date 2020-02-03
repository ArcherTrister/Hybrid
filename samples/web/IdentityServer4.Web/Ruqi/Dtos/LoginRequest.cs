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
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}