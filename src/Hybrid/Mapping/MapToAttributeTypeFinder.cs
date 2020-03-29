// -----------------------------------------------------------------------
//  <copyright file="MapToAttributeTypeFinder.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-03 23:58</last-date>
// -----------------------------------------------------------------------

using Hybrid.Reflection;


namespace Hybrid.Mapping
{
    /// <summary>
    /// 标注了<see cref="MapToAttribute"/>标签的类型查找器
    /// </summary>
    public class MapToAttributeTypeFinder : AttributeTypeFinderBase<MapToAttribute>, IMapToAttributeTypeFinder
    {
        /// <summary>
        /// 初始化一个<see cref="MapToAttributeTypeFinder"/>类型的新实例
        /// </summary>
        public MapToAttributeTypeFinder(IAllAssemblyFinder allAssemblyFinder)
            : base(allAssemblyFinder)
        { }
    }
}