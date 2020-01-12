﻿// -----------------------------------------------------------------------
//  <copyright file="AspESoftorModule.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Modules;

using Microsoft.AspNetCore.Builder;

namespace ESoftor.AspNetCore
{
    /// <summary>
    ///  基于AspNetCore环境的Module模块基类
    /// </summary>
    public abstract class AspESoftorModule : ESoftorModule
    {
        //public override IServiceCollection AddModule(IServiceCollection services)
        //{
        //    services.AddHttpContextAccessor();
        //    return services;
        //}

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