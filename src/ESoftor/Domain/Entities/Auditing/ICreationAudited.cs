// -----------------------------------------------------------------------
//  <copyright file="ICreationAudited.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

namespace ESoftor.Domain.Entities.Auditing
{
    /// <summary>
    /// 定义创建审计信息
    /// </summary>
    public interface ICreationAudited<TUserKey> : ICreatedTime
        where TUserKey : struct
    {
        /// <summary>
        /// 获取或设置 创建者编号
        /// </summary>
        TUserKey? CreatorId { get; set; }
    }
}