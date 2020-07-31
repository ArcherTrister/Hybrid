using Hybrid.Core.Packs;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace Hybrid.WeChat
{
    public abstract class WeChatPackBase : HybridPack
    {
        /// <summary>
        /// 
        /// </summary>
        public override PackLevel Level => PackLevel.Application;
        
        /// <summary>
        /// 
        /// </summary>
        public override int Order => 5;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            return base.AddServices(services);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public override void UsePack(IServiceProvider provider)
        {
            base.UsePack(provider);
        }
    }
}
