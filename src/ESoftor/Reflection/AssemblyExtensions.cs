// -----------------------------------------------------------------------
//  <copyright file="AssemblyExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2014-09-08 7:46</last-date>
// -----------------------------------------------------------------------

using ESoftor.Extensions;

using Microsoft.Extensions.DependencyModel;

using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ESoftor.Reflection
{
    /// <summary>
    /// 程序集扩展操作类
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 获取程序集的文件版本
        /// </summary>
        public static string GetFileVersion(this Assembly assembly)
        {
            assembly.CheckNotNull("assembly");
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
            return info.FileVersion;
        }

        /// <summary>
        /// 获取程序集的产品版本
        /// </summary>
        public static string GetProductVersion(this Assembly assembly)
        {
            assembly.CheckNotNull("assembly");
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
            return info.ProductVersion;
        }

        /// <summary>
        /// 获取CLI版本号
        /// </summary>
        public static string GetCliVersion()
        {
            string[] dllNames =
            {
                "Microsoft.EntityFrameworkCore",
                "Microsoft.Extensions.Configuration.Binder",
                "Microsoft.Extensions.DependencyInjection",
                "Microsoft.Extensions.DependencyInjection.Abstractions",
                "Microsoft.Extensions.Configuration.Abstractions"
            };
            CompilationLibrary lib = null;
            foreach (string dllName in dllNames)
            {
                lib = DependencyContext.Default.CompileLibraries.FirstOrDefault(m => m.Name == dllName);
                if (lib != null)
                {
                    break;
                }
            }
            string cliVersion = lib?.Version;
            return cliVersion;
        }
    }
}