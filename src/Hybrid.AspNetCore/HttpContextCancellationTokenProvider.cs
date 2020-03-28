using Hybrid.Dependency;
using Hybrid.Threading;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using System.Threading;

namespace Hybrid.AspNetCore
{
    /// <summary>
    /// 基于当前HttpContext的<see cref="IServiceScope"/>的异步任务取消标识
    /// </summary>
    [Dependency(ServiceLifetime.Singleton, ReplaceExisting = true)]
    public class HttpContextCancellationTokenProvider : ICancellationTokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 初始化一个<see cref="HttpContextCancellationTokenProvider"/>类型的新实例
        /// </summary>
        public HttpContextCancellationTokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 获取 异步任务取消标识
        /// </summary>
        public CancellationToken Token => _httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;
    }
}