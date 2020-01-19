// -----------------------------------------------------------------------
//  <copyright file="OAuth2Options.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-03-31 18:18</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Hybrid.Core.Options
{
    /// <summary>
    /// 第三方OAuth2登录的配置选项
    /// </summary>
    public sealed class OAuth2Options
    {
        /// <summary>
        /// 获取或设置 本应用在第三方OAuth2系统中的客户端Id
        /// </summary>
        [Required(ErrorMessage = "第三方OAuth2系统客户端Id不能为空")]
        public string ClientId { get; set; }

        /// <summary>
        /// 获取或设置 本应用在第三方OAuth2系统中的客户端密钥
        /// </summary>
        [Required(ErrorMessage = "第三方OAuth2系统客户端密钥不能为空")]
        public string ClientSecret { get; set; }
    }
}