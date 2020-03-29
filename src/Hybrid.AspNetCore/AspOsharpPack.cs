// -----------------------------------------------------------------------
//  <copyright file="AspHybridPack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-09 22:20</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;

using Hybrid.Core.Packs;


namespace Hybrid.AspNetCore
{
    /// <summary>
    ///  基于AspNetCore环境的Pack模块基类
    /// </summary>
    public abstract class AspHybridPack : HybridPack
    {
        /// <summary>
        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        public virtual void UsePack(IApplicationBuilder app)
        {
            base.UsePack(app.ApplicationServices);
        }
    }
}