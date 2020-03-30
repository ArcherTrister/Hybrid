using Hybrid.Identity.JwtBearer;

using System.Collections.Generic;

namespace Hybrid.Identity
{
    /// <summary>
    /// 在线用户信息基类
    /// </summary>
    public class OnlineUser
    {
        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 获取或设置 用户Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置 用户头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 获取或设置 是否管理
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 获取或设置 下次请求是否刷新AccessToken
        /// </summary>
        public bool IsRefreshNext { get; set; }

        /// <summary>
        /// 获取或设置 用户角色
        /// </summary>
        public string[] Roles { get; set; } = new string[0];

        /// <summary>
        /// 获取 扩展数据字典
        /// </summary>
        public IDictionary<string, string> ExtendData { get; } = new Dictionary<string, string>();

        /// <summary>
        /// 获取或设置 客户端刷新Token
        /// </summary>
        public IDictionary<string, RefreshToken> RefreshTokens { get; set; }
    }
}