// -----------------------------------------------------------------------
//  <copyright file="IMapperConfiguration.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-07-04 0:22</last-date>
// -----------------------------------------------------------------------

using AutoMapper.Configuration;

using Hybrid.Dependency;

namespace Hybrid.AutoMapper
{
    /// <summary>
    /// 定义通过<see cref="MapperConfigurationExpression"/>配置对象映射的功能
    /// </summary>
    [MultipleDependency]
    public interface IAutoMapperConfiguration
    {
        /// <summary>
        /// 创建对象映射
        /// </summary>
        /// <param name="mapper">映射配置表达</param>
        void CreateMaps(MapperConfigurationExpression mapper);
    }
}