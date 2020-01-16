// -----------------------------------------------------------------------
//  <copyright file="ConfirmEmailDto.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Hybrid.Web.Identity.Dtos
{
    /// <summary>
    /// 确认邮箱DTO
    /// </summary>
    public class ConfirmEmailDto
    {
        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// 获取或设置 邮件码
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}