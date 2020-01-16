// -----------------------------------------------------------------------
//  <copyright file="ModuleOutputDto.cs" company="cn.lxking">
//      Copyright ? 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-13 14:59</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Modules;
using Hybrid.Domain.Entities;

using System.ComponentModel;

namespace Hybrid.Zero.Systems.Dtos
{
    /// <summary>
    /// ���DTO��ģ�����Ϣ
    /// </summary>
    public class ModuleOutputDto : IOutputDto
    {
        /// <summary>
        /// ��ȡ������ ����
        /// </summary>
        [DisplayName("����")]
        public string Name { get; set; }

        /// <summary>
        /// ��ȡ������ ��ʾ����
        /// </summary>
        [DisplayName("��ʾ����")]
        public string Display { get; set; }

        /// <summary>
        /// ��ȡ������ ����·��
        /// </summary>
        [DisplayName("����·��")]
        public string Class { get; set; }

        /// <summary>
        /// ��ȡ������ ģ�鼶��
        /// </summary>
        [DisplayName("����")]
        public ModuleLevel Level { get; set; }

        /// <summary>
        /// ��ȡ������ ����˳��
        /// </summary>
        [DisplayName("����˳��")]
        public int Order { get; set; }

        /// <summary>
        /// ��ȡ������ �Ƿ�����
        /// </summary>
        [DisplayName("�Ƿ�����")]
        public bool IsEnabled { get; set; }
    }
}