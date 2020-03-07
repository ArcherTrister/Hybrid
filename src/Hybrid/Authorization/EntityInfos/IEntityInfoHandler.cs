// -----------------------------------------------------------------------
//  <copyright file="IEntityInfoHandler.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-14 12:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Domain.Entities;

using System;

namespace Hybrid.Core.EntityInfos
{
    /// <summary>
    /// 实体信息处理器
    /// </summary>
    public interface IEntityInfoHandler
    {
        /// <summary>
        /// 从程序集中刷新实体信息（实现了<see cref="IEntity{TKey}"/>接口的实体类）
        /// </summary>
        void Initialize();

        /// <summary>
        /// 查找指定实体类型的实体信息
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <returns>实体信息</returns>
        IEntityInfo GetEntityInfo(Type type);

        /// <summary>
        /// 查找指定实体类型的实体信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <returns>实体信息</returns>
        IEntityInfo GetEntityInfo<TEntity, TKey>() where TEntity : IEntity<TKey> where TKey : IEquatable<TKey>;

        /// <summary>
        /// 刷新实体信息缓存
        /// </summary>
        void RefreshCache();
    }
}