// -----------------------------------------------------------------------
//  <copyright file="CodeGenerationPack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-01-06 22:26</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Hybrid.Core.Packs;


namespace Hybrid.CodeGeneration
{
    /// <summary>
    /// 代码生成模块
    /// </summary>
    [Description("代码生成模块")]
    public class CodeGenerationPack : HybridPack
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level { get; } = PackLevel.Application;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.TryAddSingleton<ICodeGenerator, RazorCodeGenerator>();

            return services;
        }
    }
}