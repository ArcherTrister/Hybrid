// -----------------------------------------------------------------------
//  <copyright file="ProfileEditInputDto.cs" company="cn.lxking">
//      Copyright © 2019-2020-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-03-24 13:05</last-date>
// -----------------------------------------------------------------------

using Hybrid.Domain.Entities;
using Hybrid.Mapping;
using Hybrid.Web.Identity.Entity;

using System;
using System.ComponentModel.DataAnnotations;

namespace Hybrid.Web.Identity.Dtos
{
    /// <summary>
    /// 输入DTO：用户资料编辑
    /// </summary>
    [MapTo(typeof(User))]
    public class ProfileEditDto : IInputDto<Guid>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置 用户昵称
        /// </summary>
        [Required]
        public string NickName { get; set; }

        /// <summary>
        /// 获取或设置 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置 头像
        /// </summary>
        public string HeadImg { get; set; }
    }
}