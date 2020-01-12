// -----------------------------------------------------------------------
//  <copyright file="ConfigurationExtensions.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-10-05 19:30</last-date>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Configuration;

namespace ESoftor.Core.Options
{
    /// <summary>
    /// <see cref="IConfiguration"/>扩展方法
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 从<see cref="IConfiguration"/>创建<see cref="ESoftorOptions"/>
        /// </summary>
        public static ESoftorOptions GetESoftorOptions(this IConfiguration configuration)
        {
            ESoftorOptions options = new ESoftorOptions();
            new ESoftorOptionsSetup(configuration).Configure(options);
            return options;
        }
    }
}