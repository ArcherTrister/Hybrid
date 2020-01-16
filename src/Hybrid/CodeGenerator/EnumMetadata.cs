// -----------------------------------------------------------------------
//  <copyright file="EnumMetadata.cs" company="com.esoftor">
//      Copyright ? 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-06 12:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Extensions;

using System;

namespace Hybrid.CodeGenerator
{
    /// <summary>
    /// 枚举类型元数据
    /// </summary>
    public class EnumMetadata
    {
        /// <summary>
        /// 初始化一个<see cref="EnumMetadata"/>类型的新实例
        /// </summary>
        public EnumMetadata()
        { }

        /// <summary>
        /// 初始化一个<see cref="EnumMetadata"/>类型的新实例
        /// </summary>
        public EnumMetadata(Enum enumItem)
        {
            if (enumItem == null)
            {
                return;
            }
            Value = enumItem.CastTo<int>();
            Name = enumItem.ToString();
            Display = enumItem.ToDescription();
        }

        /// <summary>
        /// 获取或设置 枚举值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 获取或设置 枚举名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 显示名称
        /// </summary>
        public string Display { get; set; }
    }
}