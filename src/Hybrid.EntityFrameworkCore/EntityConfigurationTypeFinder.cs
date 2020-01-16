// -----------------------------------------------------------------------
//  <copyright file="EntityConfigurationTypeFinder.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-03-08 3:05</last-date>
// -----------------------------------------------------------------------

using Hybrid.Dependency;
using Hybrid.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace Hybrid.EntityFrameworkCore
{
    /// <summary>
    /// 实体类配置类型查找器
    /// </summary>
    [Dependency(ServiceLifetime.Singleton, TryAdd = true)]
    public class EntityConfigurationTypeFinder : BaseTypeFinderBase<IEntityRegister>, IEntityConfigurationTypeFinder
    {
        /// <summary>
        /// 初始化一个<see cref="BaseTypeFinderBase{TBaseType}"/>类型的新实例
        /// </summary>
        public EntityConfigurationTypeFinder(IAllAssemblyFinder allAssemblyFinder)
            : base(allAssemblyFinder)
        { }
    }
}