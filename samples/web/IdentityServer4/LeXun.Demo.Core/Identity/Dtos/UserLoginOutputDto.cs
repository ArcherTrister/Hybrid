// -----------------------------------------------------------------------
//  <copyright file="UserLoginOutputDto.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-03-24 15:11</last-date>
// -----------------------------------------------------------------------

using Hybrid.Entity;

using System;

namespace LeXun.Demo.Identity.Dtos
{
    /// <summary>
    /// 输出DTO：OAuth2登录信息
    /// </summary>
    public class UserLoginOutputDto : IOutputDto
    {
        /// <summary>
        /// 获取或设置 编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 提供商
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// 获取或设置 第三方登录用户昵称
        /// </summary>
        public string ProviderDisplayName { get; set; }

        /// <summary>
        /// 获取或设置 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 所属用户编号
        /// </summary>
        public int UserId { get; set; }
    }
}