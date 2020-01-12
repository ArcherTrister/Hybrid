// -----------------------------------------------------------------------
//  <copyright file="IAspUseModule.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;

namespace ESoftor.AspNetCore
{
    /// <summary>
    /// 定义AspNetCore环境下的应用模块服务
    /// </summary>
    public interface IAspUseModule
    {
        /// <summary>
        /// 应用模块服务，仅在AspNetCore环境下调用，非AspNetCore环境请执行 UseModule(IServiceProvider) 功能
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        void UseModule(IApplicationBuilder app);
    }
}