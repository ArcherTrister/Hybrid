// -----------------------------------------------------------------------
//  <copyright file="ModuleOutputDto.cs" company="com.esoftor">
//      Copyright ? 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-13 14:59</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Modules;
using ESoftor.Domain.Entities;

using System.ComponentModel;

namespace ESoftor.Permission.Systems.Dtos
{
    /// <summary>
    /// 输出DTO：模块包信息
    /// </summary>
    public class ModuleOutputDto : IOutputDto
    {
        /// <summary>
        /// 获取或设置 名称
        /// </summary>
        [DisplayName("名称")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 显示名称
        /// </summary>
        [DisplayName("显示名称")]
        public string Display { get; set; }

        /// <summary>
        /// 获取或设置 类型路径
        /// </summary>
        [DisplayName("类型路径")]
        public string Class { get; set; }

        /// <summary>
        /// 获取或设置 模块级别
        /// </summary>
        [DisplayName("级别")]
        public ModuleLevel Level { get; set; }

        /// <summary>
        /// 获取或设置 启动顺序
        /// </summary>
        [DisplayName("启动顺序")]
        public int Order { get; set; }

        /// <summary>
        /// 获取或设置 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public bool IsEnabled { get; set; }
    }
}