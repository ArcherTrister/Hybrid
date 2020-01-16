// -----------------------------------------------------------------------
//  <copyright file="EntityInfoHandler.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-14 18:27</last-date>
// -----------------------------------------------------------------------

using Hybrid.Dependency;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace Hybrid.Core.EntityInfos
{
    /// <summary>
    /// 实体信息处理器
    /// </summary>
    [Dependency(ServiceLifetime.Singleton, TryAdd = true)]
    public class EntityInfoHandler : EntityInfoHandlerBase<EntityInfo, EntityInfoHandler>
    {
        /// <summary>
        /// 初始化一个<see cref="EntityInfoHandlerBase{TEntityInfo,TEntityInfoProvider}"/>类型的新实例
        /// </summary>
        public EntityInfoHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        { }
    }
}