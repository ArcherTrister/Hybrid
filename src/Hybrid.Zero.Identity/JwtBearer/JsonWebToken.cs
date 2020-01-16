// -----------------------------------------------------------------------
//  <copyright file="JsonWebToken.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-06-12 15:31</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Identity.JwtBearer
{
    /// <summary>
    /// JwtToken模型
    /// </summary>
    public class JsonWebToken
    {
        /// <summary>
        /// 获取或设置 用于业务身份认证的AccessToken
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 获取或设置 用于刷新AccessToken的RefreshToken
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 获取或设置 RefreshToken有效期，UTC标准
        /// </summary>
        public long RefreshUctExpires { get; set; }
    }
}