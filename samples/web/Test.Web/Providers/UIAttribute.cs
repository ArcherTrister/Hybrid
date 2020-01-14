// -----------------------------------------------------------------------
//  <copyright file="UIAttribute" company="cn.lxking">
//      Copyright (c) 2014 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-13 22:50:48</last-date>
// -----------------------------------------------------------------------

using System;

namespace WebApplication123
{
    internal class UIAttribute: Attribute
    {
        public UIAttribute(Type implementationTemplate)
        {
            Template = implementationTemplate;
        }

        public Type Template { get; }
    }
}
