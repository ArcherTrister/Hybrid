// -----------------------------------------------------------------------
//  <copyright file="Decorator" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-01-12 17:14:21</last-date>
// -----------------------------------------------------------------------

using System;

namespace ESoftor.Zero.IdentityServer4.Identity
{
    internal class Decorator<TService>
    {
        public TService Instance { get; set; }

        public Decorator(TService instance)
        {
            Instance = instance;
        }
    }

    internal class Decorator<TService, TImpl> : Decorator<TService>
        where TImpl : class, TService
    {
        public Decorator(TImpl instance) : base(instance)
        {
        }
    }

    internal class DisposableDecorator<TService> : Decorator<TService>, IDisposable
    {
        public DisposableDecorator(TService instance) : base(instance)
        {
        }

        public void Dispose()
        {
            (Instance as IDisposable)?.Dispose();
        }
    }
}