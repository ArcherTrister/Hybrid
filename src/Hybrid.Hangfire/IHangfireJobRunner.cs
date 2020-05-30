// -----------------------------------------------------------------------
//  <copyright file="IHangfireJobRunner.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-01-02 16:17</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Hangfire
{
    /// <summary>
    /// Hangfire作业运行器
    /// </summary>
    public interface IHangfireJobRunner
    {
        /// <summary>
        /// 开始运行
        /// </summary>
        void Start();
    }
}