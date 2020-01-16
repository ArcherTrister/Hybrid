// -----------------------------------------------------------------------
//  <copyright file="DbContextModelCache.cs" company="cn.lxking">
//      Copyright ? 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-12 14:14</last-date>
// -----------------------------------------------------------------------

using Hybrid.Dependency;
using Hybrid.Extensions;

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Concurrent;

namespace Hybrid.EntityFrameworkCore
{
    /// <summary>
    /// ����������ģ�ͻ���
    /// </summary>
    [Dependency(ServiceLifetime.Singleton, AddSelf = true)]
    public class DbContextModelCache
    {
        private readonly ConcurrentDictionary<Type, IModel> _dict = new ConcurrentDictionary<Type, IModel>();

        /// <summary>
        /// ��ȡָ�����������͵�ģ��
        /// </summary>
        /// <param name="dbContextType">����������</param>
        /// <returns>����ģ��</returns>
        public IModel Get(Type dbContextType)
        {
            return _dict.GetOrDefault(dbContextType);
        }

        /// <summary>
        /// ����ָ�����������͵�ģ��
        /// </summary>
        /// <param name="dbContextType">����������</param>
        /// <param name="model">ģ��</param>
        public void Set(Type dbContextType, IModel model)
        {
            _dict[dbContextType] = model;
        }

        /// <summary>
        /// �Ƴ�ָ�����������͵�ģ��
        /// </summary>
        public void Remove(Type dbContextType)
        {
            _dict.TryRemove(dbContextType, out IModel model);
        }
    }
}