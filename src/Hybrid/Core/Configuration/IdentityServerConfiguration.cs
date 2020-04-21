﻿using System.ComponentModel.DataAnnotations;

namespace Hybrid.Core.Configuration
{
    /// <summary>
    /// IdentityServer4身份认证选项
    /// </summary>
    public sealed class IdentityServerConfiguration : IIdentityServerConfiguration
    {
        /// <summary>
        /// 获取或设置 授权服务器地址
        /// </summary>
        [Required(ErrorMessage = "授权服务器地址不能为空")]
        public string Authority { get; set; }

        /// <summary>
        /// 获取或设置 订阅方(要验证的Api)，多个用逗号隔开
        /// </summary>
        [Required(ErrorMessage = "验证令牌的Api不能为空")]
        public string Audience { get; set; }

        /// <summary>
        /// 获取或设置 是否使用ssl
        /// </summary>
        public bool UseHttps { get; set; }

        /// <summary>
        /// 获取或设置 授权服务器与Api集成/授权服务器则设置为true,如果只有Api则设置为false
        /// </summary>
        public bool IsLocalApi { get; set; }

        /// <summary>
        /// 证书路径
        /// </summary>
        public string CerPath { get; set; }

        /// <summary>
        /// 证书密码
        /// </summary>
        public string CerPwd { get; set; }

        /// <summary>
        /// 获取或设置 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}