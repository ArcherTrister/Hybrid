// -----------------------------------------------------------------------
//  <copyright file="IHybridDefaultUIAttributeTypeFinder.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.AspNetCore.UI;
using Hybrid.Reflection;
using System.Collections.Generic;
using System.Reflection;

namespace Hybrid.Zero.IdentityServer4.Quickstart
{
    /// <summary>
    /// 标注了<see cref="HybridDefaultUIAttribute"/>标签的类型查找器
    /// </summary>
    internal interface IHybridDefaultUIAttributeTypeFinder : ITypeFinder
    {
        /// <summary>
        /// 获取TypeInfo列表
        /// </summary>
        IEnumerable<TypeInfo> GetTypeInfos();
    }
}