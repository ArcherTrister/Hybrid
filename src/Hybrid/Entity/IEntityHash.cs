// -----------------------------------------------------------------------
//  <copyright file="IEntityHash.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-12 9:31</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Entity
{
    /// <summary>
    /// 定义实体Hash功能，对实体的属性值进行Hash，确定实体是否存在变化，
    /// 这些变化可用于系统初始化时确定是否需要进行数据同步
    /// </summary>
    public interface IEntityHash
    { }
}