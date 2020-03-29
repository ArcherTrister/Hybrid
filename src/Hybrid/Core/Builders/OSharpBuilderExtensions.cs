// -----------------------------------------------------------------------
//  <copyright file="HybridBuilderExtensions.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-02 14:27</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Packs;
using Hybrid.Dependency;
using Hybrid.EventBuses;


namespace Hybrid.Core.Builders
{
    /// <summary>
    /// IHybridBuilder扩展方法
    /// </summary>
    public static class HybridBuilderExtensions
    {
        /// <summary>
        /// 添加核心模块
        /// </summary>
        internal static IHybridBuilder AddCorePack(this IHybridBuilder builder)
        {
            builder.AddPack<HybridCorePack>()
                .AddPack<DependencyPack>()
                .AddPack<EventBusPack>();

            return builder;
        }
    }
}