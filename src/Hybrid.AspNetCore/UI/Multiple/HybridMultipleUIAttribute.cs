// -----------------------------------------------------------------------
//  <copyright file="HybridDefaultUIAttribute.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.AspNetCore.UI.Multiple
{
    internal class HybridMultipleUIAttribute : Attribute
    {
        public HybridMultipleUIAttribute(Type implementationTemplate, Type keyType)
        {
            Template = implementationTemplate;
            KeyType = keyType;
        }

        public Type Template { get; }

        public Type KeyType { get; }
    }
}
