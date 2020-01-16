// -----------------------------------------------------------------------
//  <copyright file="IHybridDefaultUIAttributeTypeFinder.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.AspNetCore.UI;
using ESoftor.Reflection;
using System.Collections.Generic;
using System.Reflection;

namespace ESoftor.Zero.IdentityServer4.Quickstart
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