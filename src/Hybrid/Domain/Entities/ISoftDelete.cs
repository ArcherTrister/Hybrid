// -----------------------------------------------------------------------
//  <copyright file="ISoftDelete.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Domain.Entities
{
    /// <summary>
    /// 定义逻辑删除功能
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 获取或设置 数据逻辑删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}