// -----------------------------------------------------------------------
//  <copyright file="RefreshToken.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-06-02 3:52</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.Identity.JwtBearer
{
    /// <summary>
    /// 刷新Token信息
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// 获取或设置 客户端Id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 获取或设置 标识值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        public DateTime EndUtcTime { get; set; }
    }
}