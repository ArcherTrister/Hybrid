using Hybrid.Core.Packs;

using Microsoft.Extensions.DependencyInjection;

namespace Hybrid.AspNetCore.WebApi
{
    /// <summary>
    /// WebApi模块
    /// </summary>
    [DependsOnPacks(typeof(AspNetCorePack))]
    public abstract class DynamicWebApiPackBase : AspHybridPack
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Application;

        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，同一级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 100;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddDynamicWebApi();
            return services;
        }
    }
}