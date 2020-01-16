﻿// -----------------------------------------------------------------------
//  <copyright file="IESoftorBuilder.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-03-09 12:22</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Modules;
using Hybrid.Core.Options;

using System;
using System.Collections.Generic;

namespace Hybrid.Core.Builders
{
    /// <summary>
    /// 定义Hybrid构建器
    /// </summary>
    public interface IESoftorBuilder
    {
        /// <summary>
        /// 获取 加载的模块集合
        /// </summary>
        IEnumerable<Type> AddModules { get; }

        /// <summary>
        /// 获取 排除的模块集合
        /// </summary>
        IEnumerable<Type> ExceptModules { get; }

        /// <summary>
        /// 获取 Hybrid选项配置委托
        /// </summary>
        Action<ESoftorOptions> OptionsAction { get; }

        /// <summary>
        /// 添加指定模块
        /// </summary>
        /// <typeparam name="TModule">要添加的模块类型</typeparam>
        IESoftorBuilder AddModule<TModule>() where TModule : ESoftorModule;

        /// <summary>
        /// 排除指定模块
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <returns></returns>
        IESoftorBuilder ExceptModule<TModule>() where TModule : ESoftorModule;

        /// <summary>
        /// 添加Hybrid选项配置
        /// </summary>
        /// <param name="optionsAction">Hybrid操作选项</param>
        /// <returns>Hybrid构建器</returns>
        IESoftorBuilder AddOptions(Action<ESoftorOptions> optionsAction);
    }
}