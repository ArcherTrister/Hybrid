// -----------------------------------------------------------------------
//  <copyright file="AbstractBuilder.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2014:07:08 13:09</last-date>
// -----------------------------------------------------------------------

using Hybrid.Reflection;

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Hybrid.Extensions
{
    /// <summary>
    /// 枚举<see cref="Enum"/>的扩展辅助操作方法
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举项上的<see cref="DescriptionAttribute"/>特性的文字描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum value)
        {
            Type type = value.GetType();
            MemberInfo member = type.GetMember(value.ToString()).FirstOrDefault();
            return member != null ? member.GetDescription() : value.ToString();
        }
    }
}