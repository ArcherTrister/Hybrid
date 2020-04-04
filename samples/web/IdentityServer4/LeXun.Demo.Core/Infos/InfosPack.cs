// -----------------------------------------------------------------------
// <once-generated>
//     这个文件只生成一次，再次生成不会被覆盖。
//     可以在此类的AddServices方法给“Infos”模块添加自定义服务配对，或者在UsePack方法进行模块初始化
// </once-generated>
//
//  <copyright file="IInfosPack.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
// -----------------------------------------------------------------------

using Hybrid.Core.Packs;

using LeXun.Demo.Infos.Events;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System.ComponentModel;

namespace LeXun.Demo.Infos
{
    /// <summary>
    /// 信息模块
    /// </summary>
    [Description("信息模块")]
    public class InfosPack : HybridPack
    {
        /// <summary>将模块服务添加到依赖注入服务容器中</summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.TryAddScoped<IInfosContract, InfosService>();
            services.AddEventHandler<MessageCreatedEventHandler>();

            return services;
        }
    }
}