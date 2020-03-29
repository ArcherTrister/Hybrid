// -----------------------------------------------------------------------
//  <copyright file="IEventBus.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-01-12 14:12</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.EventBuses
{
    /// <summary>
    /// 定义线程总线
    /// </summary>
    public interface IEventBus : IEventSubscriber, IEventPublisher
    {
        
    }
}