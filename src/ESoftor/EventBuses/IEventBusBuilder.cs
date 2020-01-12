// -----------------------------------------------------------------------
//  <copyright file="IEventBusBuilder.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-19 1:54</last-date>
// -----------------------------------------------------------------------

namespace ESoftor.EventBuses
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