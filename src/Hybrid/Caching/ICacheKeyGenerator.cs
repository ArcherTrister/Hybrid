﻿// -----------------------------------------------------------------------
//  <copyright file="ICacheKeyGenerator.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2016-11-16 23:41</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Caching
{
    /// <summary>
    /// 定义缓存键生成功能
    /// </summary>
    public interface ICacheKeyGenerator
    {
        /// <summary>
        /// 生成缓存键
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns></returns>
        string GetKey(params object[] args);
    }
}