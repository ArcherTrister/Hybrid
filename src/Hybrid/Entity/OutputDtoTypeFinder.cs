// -----------------------------------------------------------------------
//  <copyright file="OutputDtoTypeFinder.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-06 10:16</last-date>
// -----------------------------------------------------------------------

using Hybrid.Reflection;

namespace Hybrid.Entity
{
    /// <summary>
    /// <see cref="IOutputDto"/>类型查找器
    /// </summary>
    public class OutputDtoTypeFinder : BaseTypeFinderBase<IOutputDto>, IOutputDtoTypeFinder
    {
        /// <summary>
        /// 初始化一个<see cref="BaseTypeFinderBase{TBaseType}"/>类型的新实例
        /// </summary>
        public OutputDtoTypeFinder(IAllAssemblyFinder allAssemblyFinder)
            : base(allAssemblyFinder)
        { }
    }
}