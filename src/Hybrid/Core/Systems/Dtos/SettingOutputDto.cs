// -----------------------------------------------------------------------
//  <copyright file="SettingOutputDto.cs" company="cn.lxking">
//      Copyright ? 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-24 17:26</last-date>
// -----------------------------------------------------------------------

using Hybrid.Extensions;

namespace Hybrid.Core.Systems.Dtos
{
    /// <summary>
    /// �������DTO
    /// </summary>
    public class SettingOutputDto
    {
        /// <summary>
        /// ��ʼ��һ��<see cref="SettingOutputDto"/>���͵���ʵ��
        /// </summary>
        public SettingOutputDto(ISetting setting)
        {
            Setting = setting;
            SettingTypeName = setting.GetType().GetFullNameWithModule();
        }

        /// <summary>
        /// ��ȡ ��������ȫ��
        /// </summary>
        public string SettingTypeName { get; }

        /// <summary>
        /// ��ȡ ������Ϣ
        /// </summary>
        public ISetting Setting { get; }
    }
}