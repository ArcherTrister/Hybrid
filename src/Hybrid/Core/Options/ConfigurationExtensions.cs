// -----------------------------------------------------------------------
//  <copyright file="ConfigurationExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-10-05 19:30</last-date>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Configuration;

namespace Hybrid.Core.Options
{
    /// <summary>
    /// <see cref="IConfiguration"/>扩展方法
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 从<see cref="IConfiguration"/>创建<see cref="HybridOptions"/>
        /// </summary>
        public static HybridOptions GetHybridOptions(this IConfiguration configuration)
        {
            HybridOptions options = new HybridOptions();
            new HybridOptionsSetup(configuration).Configure(options);
            return options;
        }
    }
}