// -----------------------------------------------------------------------
//  <copyright file="ESoftorBuilder.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 15:40</last-date>
// -----------------------------------------------------------------------

using ESoftor.Collections;
using ESoftor.Core.Modules;
using ESoftor.Core.Options;
using ESoftor.Data;

using System;
using System.Collections.Generic;
using System.Linq;

namespace ESoftor.Core.Builders
{
    /// <summary>
    /// ESoftor构建器
    /// </summary>
    public class ESoftorBuilder : IESoftorBuilder
    {
        /// <summary>
        /// 初始化一个<see cref="ESoftorBuilder"/>类型的新实例
        /// </summary>
        public ESoftorBuilder()
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
        /// 获取 ESoftor选项配置委托
        /// </summary>
        public Action<ESoftorOptions> OptionsAction { get; private set; }

        /// <summary>
        /// 添加指定模块，执行此功能后将仅加载指定的模块
        /// </summary>
        /// <typeparam name="TModule">要添加的模块类型</typeparam>
        public IESoftorBuilder AddModule<TModule>() where TModule : ESoftorModule
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
        public IESoftorBuilder ExceptModule<TModule>() where TModule : ESoftorModule
        {
            List<Type> list = ExceptModules.ToList();
            list.AddIfNotExist(typeof(TModule));
            ExceptModules = list;
            return this;
        }

        /// <summary>
        /// 添加ESoftor选项配置
        /// </summary>
        /// <param name="optionsAction">ESoftor操作选项</param>
        /// <returns>ESoftor构建器</returns>
        public IESoftorBuilder AddOptions(Action<ESoftorOptions> optionsAction)
        {
            Check.NotNull(optionsAction, nameof(optionsAction));
            OptionsAction = optionsAction;
            return this;
        }
    }
}