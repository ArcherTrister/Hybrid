// -----------------------------------------------------------------------
//  <copyright file="IEntity.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Domain.Entities
{
    /// <summary>
    /// 数据模型接口
    /// </summary>
    public interface IEntity<out TKey>
    {
        /// <summary>
        /// 获取 实体唯一标识，主键
        /// </summary>
        TKey Id { get; }
    }
}