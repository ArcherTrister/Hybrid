// -----------------------------------------------------------------------
//  <copyright file="HybridModule.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2018-07-25 12:03</last-date>
// -----------------------------------------------------------------------

using Hybrid.Extensions;
using Hybrid.Reflection;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Hybrid.Core.Modules
{
    /// <summary>
    /// Hybrid模块基类
    /// </summary>
    [Description("Hybrid模块")]
    public abstract class HybridModule
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public virtual ModuleLevel Level => ModuleLevel.Business;

        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，同一级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public virtual int Order => 0;

        /// <summary>
        /// 获取 是否已可用
        /// </summary>
        public bool IsEnabled { get; protected set; }

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public virtual IServiceCollection AddServices(IServiceCollection services)
        {
            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public virtual void UseModule(IServiceProvider provider)
        {
            IsEnabled = true;
        }

        /// <summary>
        /// 获取当前模块的依赖模块类型
        /// </summary>
        /// <returns></returns>
        internal Type[] GetDependModuleTypes(Type moduleType = null)
        {
            if (moduleType == null)
            {
                moduleType = GetType();
            }
            DependsOnModulesAttribute[] dependAttrs = moduleType.GetAttributes<DependsOnModulesAttribute>();
            if (dependAttrs.Length == 0)
            {
                return new Type[0];
            }
            List<Type> dependTypes = new List<Type>();
            foreach (DependsOnModulesAttribute dependAttr in dependAttrs)
            {
                Type[] moduleTypes = dependAttr.DependedModuleTypes;
                if (moduleTypes.Length == 0)
                {
                    continue;
                }
                dependTypes.AddRange(moduleTypes);
                foreach (Type type in moduleTypes)
                {
                    dependTypes.AddRange(GetDependModuleTypes(type));
                }
            }

            return dependTypes.Distinct().ToArray();
        }
    }
}