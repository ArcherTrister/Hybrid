// -----------------------------------------------------------------------
//  <copyright file="ILockable.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-08-19 2:31</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Entity
{
    /// <summary>
    /// 定义可锁定功能
    /// </summary>
    public interface ILockable
    {
        /// <summary>
        /// 获取或设置 是否锁定当前信息
        /// </summary>
        bool IsLocked { get; set; }
    }
}