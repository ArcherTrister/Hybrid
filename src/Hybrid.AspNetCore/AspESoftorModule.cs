// -----------------------------------------------------------------------
//  <copyright file="AspESoftorModule.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Modules;

using Microsoft.AspNetCore.Builder;

namespace Hybrid.AspNetCore
{
    /// <summary>
    ///  基于AspNetCore环境的Module模块基类
    /// </summary>
    public abstract class AspESoftorModule : ESoftorModule
    {
        /// <summary>
        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        public virtual void UseModule(IApplicationBuilder app)
        {
            base.UseModule(app.ApplicationServices);
        }
    }
}