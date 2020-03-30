// -----------------------------------------------------------------------
//  <copyright file="ISeedData.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-03-06 21:36</last-date>
// -----------------------------------------------------------------------

using Hybrid.Dependency;

namespace Hybrid.Entity
{
    /// <summary>
    /// 定义种子数据初始化
    /// </summary>
    [MultipleDependency]
    public interface ISeedDataInitializer
    {
        /// <summary>
        /// 获取 种子数据初始化的顺序
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 初始化种子数据
        /// </summary>
        void Initialize();
    }
}