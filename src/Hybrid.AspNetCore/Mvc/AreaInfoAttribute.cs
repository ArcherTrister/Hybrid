// -----------------------------------------------------------------------
//  <copyright file="AreaInfoAttribute.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;

namespace Hybrid.AspNetCore.Mvc
{
    /// <summary>
    /// 区域信息特性，可配置区域显示名称
    /// </summary>
    public class AreaInfoAttribute : AreaAttribute
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