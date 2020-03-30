// -----------------------------------------------------------------------
//  <copyright file="AreaInfoAttribute.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-05-29 17:30</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;

using System.ComponentModel;

namespace Hybrid.AspNetCore.Mvc
{
    /// <summary>
    /// 区域信息特性，可配置区域显示名称，此属性与“<see cref="AreaAttribute"/>与<see cref="DisplayNameAttribute"/>”组合等效，在无Area的类型，推荐只使用<see cref="DisplayNameAttribute"/>
    /// </summary>
    public sealed class AreaInfoAttribute : AreaAttribute
    {
        /// <summary>
        /// Initializes a new <see cref="T:Microsoft.AspNetCore.Mvc.AreaAttribute" /> instance.
        /// </summary>
        /// <param name="areaName">The area containing the controller or action.</param>
        public AreaInfoAttribute(string areaName)
            : base(areaName)
        { }

        /// <summary>
        /// 获取或设置 区域的显示名称
        /// </summary>
        public string Display { get; set; }
    }
}