// -----------------------------------------------------------------------
//  <copyright file="SwaggerOptions.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-12-14 23:28</last-date>
// -----------------------------------------------------------------------

namespace ESoftor.Core.Options
{
    /// <summary>
    /// Swagger选项
    /// </summary>
    public class SwaggerOptions
    {
        /// <summary>
        /// 获取或设置 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置 版本
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// 获取或设置 Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 获取或设置 是否启用
        /// </summary>
        public bool Enabled { get; set; }
    }
}