// -----------------------------------------------------------------------
//  <copyright file="NLogModule.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Modules;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System.ComponentModel;

namespace ESoftor.NLog
{
    /// <summary>
    /// NLog模块
    /// </summary>
    [Description("NLog日志模块")]
    public class NLogModule : ESoftorModule
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override ModuleLevel Level => ModuleLevel.Core;

        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 1;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();
            return services;
        }
    }
}
