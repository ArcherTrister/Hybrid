// -----------------------------------------------------------------------
//  <copyright file="UserLoginInfoEx.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Identity;

namespace Hybrid.Zero.Identity
{
    /// <summary>
    /// 第三方用户登录信息
    /// </summary>
    public class UserLoginInfoEx : UserLoginInfo
    {
        /// <summary>
        /// 初始化一个<see cref="UserLoginInfoEx"/>类型的新实例
        /// </summary>
        public UserLoginInfoEx()
            : base(null, null, null)
        { }

        /// <summary>
        /// Creates a new instance of <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" />
        /// </summary>
        /// <param name="loginProvider">The provider associated with this login information.</param>
        /// <param name="providerKey">The unique identifier for this user provided by the login provider.</param>
        /// <param name="displayName">The display name for this user provided by the login provider.</param>
        public UserLoginInfoEx(string loginProvider, string providerKey, string displayName)
            : base(loginProvider, providerKey, displayName)
        { }

        /// <summary>
        /// 获取或设置 头像URL
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 获取或设置 注册IP
        /// </summary>
        public string RegisterIp { get; set; }

        /// <summary>
        /// 获取或设置 登录账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 获取或设置 登录密码
        /// </summary>
        public string Password { get; set; }
    }
}