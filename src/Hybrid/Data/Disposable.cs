using System;

namespace Hybrid.Data
{
    /// <summary>
    /// <see cref="IDisposable"/>接口的一般实现
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        /// <summary>
        /// 这个实例被处理了吗？
        /// </summary>
        protected bool Disposed { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Disposed = true;
            }
        }

        /// <summary>执行与释放或重置非托管资源关联的应用程序定义的任务。</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Disposable()
        {
            Dispose(false);
        }
    }
}