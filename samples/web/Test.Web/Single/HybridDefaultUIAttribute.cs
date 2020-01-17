// -----------------------------------------------------------------------
//  <copyright file="UIAttribute" company="cn.lxking">
//      Copyright (c) 2014 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-13 22:50:48</last-date>
// -----------------------------------------------------------------------

using System;

namespace Test.Web.Single
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