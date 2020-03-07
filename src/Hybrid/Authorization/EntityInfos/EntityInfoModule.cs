// -----------------------------------------------------------------------
//  <copyright file="EntityInfoModule.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-23 15:25</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Modules;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.ComponentModel;

namespace Hybrid.Authorization.EntityInfos
{
    /// <summary>
    /// 实体信息模块
    /// </summary>
    [Description("数据实体模块")]
    public class EntityInfoModule : HybridModule
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override ModuleLevel Level => ModuleLevel.Application;

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UseModule(IServiceProvider provider)
        {
            IEntityInfoHandler handler = provider.GetRequiredService<IEntityInfoHandler>();
            handler.Initialize();
            IsEnabled = true;
        }
    }
}