// -----------------------------------------------------------------------
//  <copyright file="IDependencyTypeFinder.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-12-31 20:53</last-date>
// -----------------------------------------------------------------------

using ESoftor.Reflection;

namespace ESoftor.Dependency
{
    /// <summary>
    /// 依赖注入类型查找器，查找标注了<see cref="DependencyAttribute"/>特性，
    /// 或者<see cref="ISingletonDependency"/>,<see cref="IScopeDependency"/>,<see cref="ITransientDependency"/>三个接口的服务实现类型
    /// </summary>
    public interface IDependencyTypeFinder : ITypeFinder
    { }
}