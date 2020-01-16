// -----------------------------------------------------------------------
//  <copyright file="ExternalLoginInfoExtensions" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-12 14:41:49</last-date>
// -----------------------------------------------------------------------

using ESoftor.Security.Claims;

using Microsoft.AspNetCore.Identity;

using System.Security.Claims;

namespace ESoftor.Zero.Identity.Extensions
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