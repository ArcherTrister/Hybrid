// -----------------------------------------------------------------------
//  <copyright file="SendMailDto.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace LeXun.Demo.Identity.Dtos
{
    /// <summary>
    /// 发送邮件DTO
    /// </summary>
    public class SendMailDto
    {
        /// <summary>
        /// 获取或设置 Email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置 验证码
        /// </summary>
        public string VerifyCode { get; set; }

        /// <summary>
        /// 获取或设置 验证码编号
        /// </summary>
        public string VerifyCodeId { get; set; }
    }
}