﻿// -----------------------------------------------------------------------
//  <copyright file="IEventBusBuilder.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-19 1:54</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.EventBuses
{
    /// <summary>
    /// 定义事件总线构建器
    /// </summary>
    public interface IEventBusBuilder
    {
        /// <summary>
        /// 构建事件总线
        /// </summary>
        void Build();
    }
}