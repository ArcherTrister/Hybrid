// -----------------------------------------------------------------------
//  <copyright file="SettingOutputDto.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-24 17:26</last-date>
// -----------------------------------------------------------------------

using Hybrid.Reflection;

namespace Hybrid.Core.Systems
{
    /// <summary>
    /// 设置输出DTO
    /// </summary>
    public class SettingOutputDto
    {
        /// <summary>
        /// 初始化一个<see cref="SettingOutputDto"/>类型的新实例
        /// </summary>
        public SettingOutputDto(ISetting setting)
        {
            Setting = setting;
            SettingTypeName = setting.GetType().GetFullNameWithModule();
        }

        /// <summary>
        /// 获取 设置类型全名
        /// </summary>
        public string SettingTypeName { get; }

        /// <summary>
        /// 获取 设置信息
        /// </summary>
        public ISetting Setting { get; }
    }
}