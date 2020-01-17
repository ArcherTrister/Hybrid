// -----------------------------------------------------------------------
//  <copyright file="AssemblyExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-08-04 0:46</last-date>
// -----------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;

namespace Hybrid.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 嵌入式资源转字符串
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="resourceName">资源名称【全称】</param>
        /// <returns></returns>
        public static string GetStringByResource(this Assembly assembly, string resourceName)
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException(
                        $"Requested resource `{resourceName}` was not found in the assembly `{assembly}`.");
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}