﻿// -----------------------------------------------------------------------
//  <copyright file="TokenDto.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-06-13 11:59</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace ESoftor.Identity.JwtBearer.Dtos
{
    /// <summary>
    /// Token请求DTO
    /// </summary>
    public class TokenDto
    {
        /// <summary>
        /// 获取或设置 授权类型
        /// </summary>
        [Required]
        public string GrantType { get; set; }

        /// <summary>
        /// 获取或设置 账户名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 获取或设置 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置 验证码
        /// </summary>
        public string VerifyCode { get; set; }

        /// <summary>
        /// 获取或设置 刷新Token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}