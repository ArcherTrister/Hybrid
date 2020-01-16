// -----------------------------------------------------------------------
//  <copyright file="ServiceProviderExtensions.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-06-12 18:27</last-date>
// -----------------------------------------------------------------------

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace ESoftor.Extensions
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Checks whether given type is registered before.
        /// </summary>
        /// <typeparam name="TType">Type to check</typeparam>
        public static bool IsRegistered<TType>([NotNull]this IServiceProvider provider)
        {
            return provider.GetService<TType>() != null;
        }
    }
}
