// -----------------------------------------------------------------------
//  <copyright file="SettingInputDto.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-24 17:21</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;


namespace Hybrid.Core.Systems
{
    /// <summary>
    /// 设置信息输入DTO
    /// </summary>
    public class SettingInputDto
    {
        /// <summary>
        /// 获取或设置 设置类型全名
        /// </summary>
        [Required]
        public string SettingTypeName { get; set; }

        /// <summary>
        /// 获取或设置 设置模型JSON
        /// </summary>
        public string SettingJson { get; set; }
    }
}