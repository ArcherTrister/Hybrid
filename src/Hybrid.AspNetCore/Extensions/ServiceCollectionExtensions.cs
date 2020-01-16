// -----------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-01 20:39:00</last-date>
// -----------------------------------------------------------------------

using Hybrid.Dependency;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Hybrid.AspNetCore.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/>扩展方法
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 获取<see cref="IWebHostEnvironment"/>环境信息
        /// </summary>
        public static IWebHostEnvironment GetWebHostEnvironment(this IServiceCollection services)
        {
            return services.GetSingletonInstance<IWebHostEnvironment>();
        }
    }
}
