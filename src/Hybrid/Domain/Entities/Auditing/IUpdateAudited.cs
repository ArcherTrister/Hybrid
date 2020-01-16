// -----------------------------------------------------------------------
//  <copyright file="IUpdateAudited.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.Domain.Entities.Auditing
{
    /// <summary>
    /// 定义更新审计的信息
    /// </summary>
    public interface IUpdateAudited<TUserKey> where TUserKey : struct
    {
        /// <summary>
        /// 获取或设置 更新者编号
        /// </summary>
        TUserKey? LastUpdaterId { get; set; }

        /// <summary>
        /// 获取或设置 最后更新时间
        /// </summary>
        DateTime? LastUpdatedTime { get; set; }
    }
}