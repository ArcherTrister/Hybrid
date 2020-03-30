// -----------------------------------------------------------------------
//  <copyright file="JwtOptions.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-05-19 9:38</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Hybrid.Core.Options
{
    /// <summary>
    /// JWT身份认证选项
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// 获取或设置 密钥
        /// </summary>
        [Required(ErrorMessage = "密钥不能为空")]
        public string Secret { get; set; }

        /// <summary>
        /// 获取或设置 发行方
        /// </summary>
        [Required(ErrorMessage = "发行方不能为空")]
        public string Issuer { get; set; }

        /// <summary>
        /// 获取或设置 订阅方
        /// </summary>
        [Required(ErrorMessage = "订阅方不能为空")]
        public string Audience { get; set; }

        /// <summary>
        /// 获取或设置 AccessToken有效期分钟数
        /// </summary>
        public double AccessExpireMins { get; set; }

        /// <summary>
        /// 获取或设置 RefreshToken有效期分钟数
        /// </summary>
        public double RefreshExpireMins { get; set; }

        /// <summary>
        /// 获取或设置 RefreshToken是否绝对过期
        /// </summary>
        public bool IsRefreshAbsoluteExpired { get; set; } = true;

        /// <summary>
        /// 获取或设置 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}