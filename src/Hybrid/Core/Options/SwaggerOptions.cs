// -----------------------------------------------------------------------
//  <copyright file="SwaggerOptions.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-12-14 23:28</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Core.Options
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
    }
}