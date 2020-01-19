// -----------------------------------------------------------------------
//  <copyright file="ServiceProviderExtensions.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-12-19 16:58</last-date>
// -----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Hybrid.Dependency
{
    /// <summary>
    /// <see cref="IServiceProvider"/>扩展方法
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// 执行<see cref="ServiceLifetime.Scoped"/>生命周期的业务逻辑
        /// 1.当前处理<see cref="ServiceLifetime.Scoped"/>生命周期外，使用CreateScope创建<see cref="ServiceLifetime.Scoped"/>
        /// 生命周期的ServiceProvider来执行，并释放资源
        /// 2.当前处于<see cref="ServiceLifetime.Scoped"/>生命周期内，直接使用<see cref="ServiceLifetime.Scoped"/>的ServiceProvider来执行
        /// </summary>
        public static void ExecuteScopedWork(this IServiceProvider provider, Action<IServiceProvider> action, bool useHttpScope = true)
        {
            using (IServiceScope scope = useHttpScope
                ? provider.GetRequiredService<IHybridServiceScopeFactory>().CreateScope()
                : provider.CreateScope())
            {
                action(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// 异步执行<see cref="ServiceLifetime.Scoped"/>生命周期的业务逻辑
        /// 1.当前处理<see cref="ServiceLifetime.Scoped"/>生命周期外，使用CreateScope创建<see cref="ServiceLifetime.Scoped"/>
        /// 生命周期的ServiceProvider来执行，并释放资源
        /// 2.当前处于<see cref="ServiceLifetime.Scoped"/>生命周期内，直接使用<see cref="ServiceLifetime.Scoped"/>的ServiceProvider来执行
        /// </summary>
        public static async Task ExecuteScopedWorkAsync(this IServiceProvider provider, Func<IServiceProvider, Task> action, bool useHttpScope = true)
        {
            using (IServiceScope scope = useHttpScope
                ? provider.GetRequiredService<IHybridServiceScopeFactory>().CreateScope()
                : provider.CreateScope())
            {
                await action(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// 执行<see cref="ServiceLifetime.Scoped"/>生命周期的业务逻辑，并获取返回值
        /// 1.当前处理<see cref="ServiceLifetime.Scoped"/>生命周期外，使用CreateScope创建<see cref="ServiceLifetime.Scoped"/>
        /// 生命周期的ServiceProvider来执行，并释放资源
        /// 2.当前处于<see cref="ServiceLifetime.Scoped"/>生命周期内，直接使用<see cref="ServiceLifetime.Scoped"/>的ServiceProvider来执行
        /// </summary>
        public static TResult ExecuteScopedWork<TResult>(this IServiceProvider provider, Func<IServiceProvider, TResult> func, bool useHttpScope = true)
        {
            using (IServiceScope scope = useHttpScope
                ? provider.GetRequiredService<IHybridServiceScopeFactory>().CreateScope()
                : provider.CreateScope())
            {
                return func(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// 执行<see cref="ServiceLifetime.Scoped"/>生命周期的业务逻辑，并获取返回值
        /// 1.当前处理<see cref="ServiceLifetime.Scoped"/>生命周期外，使用CreateScope创建<see cref="ServiceLifetime.Scoped"/>
        /// 生命周期的ServiceProvider来执行，并释放资源
        /// 2.当前处于<see cref="ServiceLifetime.Scoped"/>生命周期内，直接使用<see cref="ServiceLifetime.Scoped"/>的ServiceProvider来执行
        /// </summary>
        public static async Task<TResult> ExecuteScopedWorkAsync<TResult>(this IServiceProvider provider, Func<IServiceProvider, Task<TResult>> func, bool useHttpScope = true)
        {
            using (IServiceScope scope = useHttpScope
                ? provider.GetRequiredService<IHybridServiceScopeFactory>().CreateScope()
                : provider.CreateScope())
            {
                return await func(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        public static ClaimsPrincipal GetCurrentUser(this IServiceProvider provider)
        {
            try
            {
                IPrincipal user = provider.GetRequiredService<IPrincipal>();
                return user as ClaimsPrincipal;
            }
            catch
            {
                return null;
            }
        }
    }
}