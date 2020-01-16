// -----------------------------------------------------------------------
//  <copyright file="IMapTuple.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
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