// -----------------------------------------------------------------------
//  <copyright file="IEventBus.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
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