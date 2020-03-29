// -----------------------------------------------------------------------
//  <copyright file="IUpdateAudited.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-04-12 10:27</last-date>
// -----------------------------------------------------------------------

using System;


namespace Hybrid.Entity
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