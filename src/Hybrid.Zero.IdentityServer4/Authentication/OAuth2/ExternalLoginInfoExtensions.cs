// -----------------------------------------------------------------------
//  <copyright file="ExternalLoginInfoExtensions.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-03-21 23:26</last-date>
// -----------------------------------------------------------------------

using System.Security.Claims;

using Microsoft.AspNetCore.Identity;


namespace Hybrid.Identity.OAuth2
{
    /// <summary>
    /// 第三方登录信息扩展
    /// </summary>
    public static class ExternalLoginInfoExtensions
    {
        /// <summary>
        /// 获取第三方登录信息
        /// </summary>
        public static UserLoginInfoEx ToUserLoginInfoEx(this ExternalLoginInfo loginInfo)
        {
            if (!(loginInfo.Principal.Identity is ClaimsIdentity identity))
            {
                return null;
            }
            string displayName = identity.GetUserName();
            UserLoginInfoEx info = new UserLoginInfoEx(loginInfo.LoginProvider, loginInfo.ProviderKey, displayName)
            {
                AvatarUrl = identity.FindFirst(m => m.Type == "urn:qq:figure")?.Value
            };

            return info;
        }
    }
}