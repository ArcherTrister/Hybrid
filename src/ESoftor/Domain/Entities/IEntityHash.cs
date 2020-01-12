// -----------------------------------------------------------------------
//  <copyright file="IEntityHash.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

namespace ESoftor.Domain.Entities
{
    /// <summary>
    /// 定义实体Hash功能，对实体的属性值进行Hash，确定实体是否存在变化，
    /// 这些变化可用于系统初始化时确定是否需要进行数据同步
    /// </summary>
    public interface IEntityHash
    { }
}