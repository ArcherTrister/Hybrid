// -----------------------------------------------------------------------
//  <copyright file="IModuleHandler.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-02-10 20:13</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Authorization.Modules
{
    /// <summary>
    /// 定义模块信息处理器，负责在系统初始化时从程序集获取最新的模块信息，并同步到数据库中
    /// </summary>
    public interface IModuleHandler
    {
        /// <summary>
        /// 从程序集中获取模块信息
        /// </summary>
        void Initialize();
    }
}