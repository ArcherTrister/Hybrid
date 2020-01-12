// -----------------------------------------------------------------------
//  <copyright file="LoginDto.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-01 20:39:00</last-date>
// -----------------------------------------------------------------------

namespace ESoftor.Web.Identity.Dto
{
    /// <summary>
    /// 登录信息DTO
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 获取或设置 登录账号，可以是用户名，Email，手机号等
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 获取或设置 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置 验证码
        /// </summary>
        public string VerifyCode { get; set; }

        /// <summary>
        /// 获取或设置 记住登录
        /// </summary>
        public bool Remember { get; set; }

        /// <summary>
        /// 获取或设置 IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 获取或设置 客户端代理头
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 获取或设置 回调地址
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}