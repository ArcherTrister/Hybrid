// -----------------------------------------------------------------------
//  <copyright file="IMapTuple.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-10-05 19:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Dependency;


namespace Hybrid.Mapping
{
    /// <summary>
    /// 定义对象映射源与目标配对
    /// </summary>
    [MultipleDependency]
    public interface IMapTuple
    {
        /// <summary>
        /// 执行对象映射构造
        /// </summary>
        void CreateMap();
    }
}