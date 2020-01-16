// -----------------------------------------------------------------------
//  <copyright file="HybridBuilder.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 15:40</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Modules;
using Hybrid.Core.Options;
using Hybrid.Data;
using Hybrid.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hybrid.Core.Builders
{
    /// <summary>
    /// Hybrid构建器
    /// </summary>
    public class HybridBuilder : IHybridBuilder
    {
        /// <summary>
        /// 初始化一个<see cref="HybridBuilder"/>类型的新实例
        /// </summary>
        public HybridBuilder()
        {
            AddModules = new List<Type>();
            ExceptModules = new List<Type>();
        }

        /// <summary>
        /// 获取 加载的模块集合
        /// </summary>
        public IEnumerable<Type> AddModules { get; private set; }

        /// <summary>
        /// 获取 排除的模块集合
        /// </summary>
        public IEnumerable<Type> ExceptModules { get; private set; }

        /// <summary>
        /// 获取 Hybrid选项配置委托
        /// </summary>
        public Action<HybridOptions> OptionsAction { get; private set; }

        /// <summary>
        /// 添加指定模块，执行此功能后将仅加载指定的模块
        /// </summary>
        /// <typeparam name="TModule">要添加的模块类型</typeparam>
        public IHybridBuilder AddModule<TModule>() where TModule : HybridModule
        {
            List<Type> list = AddModules.ToList();
            list.AddIfNotExist(typeof(TModule));
            AddModules = list;
            return this;
        }

        /// <summary>
        /// 移除指定模块，执行此功能以从自动加载的模块中排除指定模块
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <returns></returns>
        public IHybridBuilder ExceptModule<TModule>() where TModule : HybridModule
        {
            List<Type> list = ExceptModules.ToList();
            list.AddIfNotExist(typeof(TModule));
            ExceptModules = list;
            return this;
        }

        /// <summary>
        /// 添加Hybrid选项配置
        /// </summary>
        /// <param name="optionsAction">Hybrid操作选项</param>
        /// <returns>Hybrid构建器</returns>
        public IHybridBuilder AddOptions(Action<HybridOptions> optionsAction)
        {
            Check.NotNull(optionsAction, nameof(optionsAction));
            OptionsAction = optionsAction;
            return this;
        }
    }
}