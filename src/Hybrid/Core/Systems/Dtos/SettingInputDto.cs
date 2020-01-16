// -----------------------------------------------------------------------
//  <copyright file="SettingInputDto.cs" company="cn.lxking">
//      Copyright ? 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-24 17:21</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Hybrid.Core.Systems.Dtos
{
    /// <summary>
    /// ������Ϣ����DTO
    /// </summary>
    public class SettingInputDto
    {
        /// <summary>
        /// ��ȡ������ ��������ȫ��
        /// </summary>
        [Required]
        public string SettingTypeName { get; set; }

        /// <summary>
        /// ��ȡ������ ����ģ��JSON
        /// </summary>
        public string SettingJson { get; set; }
    }
}