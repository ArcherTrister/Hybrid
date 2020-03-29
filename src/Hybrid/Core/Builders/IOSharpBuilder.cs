// -----------------------------------------------------------------------
//  <copyright file="IHybridBuilder.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-03-09 12:22</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Hybrid.Core.Options;
using Hybrid.Core.Packs;


namespace Hybrid.Core.Builders
{
    /// <summary>
    /// 定义Hybrid构建器
    /// </summary>
    public interface IHybridBuilder
    {
        /// <summary>
        /// 获取 服务集合
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// 获取 加载的模块集合
        /// </summary>
        IEnumerable<HybridPack> Packs { get; }

        /// <summary>
        /// 获取 Hybrid选项配置委托
        /// </summary>
        Action<HybridOptions> OptionsAction { get; }

        /// <summary>
        /// 添加指定模块
        /// </summary>
        /// <typeparam name="TPack">要添加的模块类型</typeparam>
        IHybridBuilder AddPack<TPack>() where TPack : HybridPack;

        /// <summary>
        /// 添加Hybrid选项配置
        /// </summary>
        /// <param name="optionsAction">Hybrid操作选项</param>
        /// <returns>Hybrid构建器</returns>
        IHybridBuilder AddOptions(Action<HybridOptions>optionsAction);
    }
}