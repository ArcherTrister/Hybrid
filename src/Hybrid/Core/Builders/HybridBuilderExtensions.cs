// -----------------------------------------------------------------------
//  <copyright file="HybridBuilderExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-02 14:27</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Modules;

namespace Hybrid.Core.Builders
{
    /// <summary>
    /// IHybridBuilder扩展方法
    /// </summary>
    public static class HybridBuilderExtensions
    {
        /// <summary>
        /// 添加CoreModule
        /// </summary>
        public static IHybridBuilder AddCoreModule(this IHybridBuilder builder)
        {
            return builder.AddModule<HybridCoreModule>();
        }
    }
}