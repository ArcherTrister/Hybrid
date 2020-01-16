// -----------------------------------------------------------------------
//  <copyright file="HybridDefaultUIAttribute.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.AspNetCore.UI
{
    internal class HybridDefaultUIAttribute : Attribute
    {
        public HybridDefaultUIAttribute(Type implementationTemplate)
        {
            Template = implementationTemplate;
        }

        public Type Template { get; }
    }
}
