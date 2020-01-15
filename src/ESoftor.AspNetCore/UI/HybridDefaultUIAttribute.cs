// -----------------------------------------------------------------------
//  <copyright file="HybridDefaultUIAttribute.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using System;

namespace ESoftor.AspNetCore.UI
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
