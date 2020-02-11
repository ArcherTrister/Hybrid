namespace IdentityServer4.Web.Ruqi.Dtos
{
    /// <summary>
    /// 注册响应模型
    /// </summary>
    public sealed class RegisterResponse
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 注册IP
        /// </summary>
        public string RegisterIp { get; set; }
    }
}