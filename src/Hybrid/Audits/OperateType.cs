// -----------------------------------------------------------------------
//  <copyright file="OperateType.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 3:59</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Audits
{
    /// <summary>
    /// 表示实体审计操作类型
    /// </summary>
    public enum OperateType
    {
        /// <summary>
        /// 查询
        /// </summary>
        Query = 0,

        /// <summary>
        /// 新增
        /// </summary>
        Insert = 1,

        /// <summary>
        /// 更新
        /// </summary>
        Update = 2,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3
    }
}