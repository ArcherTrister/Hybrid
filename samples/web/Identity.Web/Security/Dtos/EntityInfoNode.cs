// -----------------------------------------------------------------------
//  <copyright file="EntityInfoNode.cs" company="ESoftor开源团队">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-07-08 3:03</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.EntityInfos;
using ESoftor.Mapping;

using System;


namespace ESoftor.Web.Security.Dtos
{
    /// <summary>
    /// 实体信息节点
    /// </summary>
    [MapFrom(typeof(EntityInfo))]
    public class EntityInfoNode
    {
        /// <summary>
        /// 获取或设置 编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 实体名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 实体类型名称
        /// </summary>
        public string TypeName { get; set; }
    }
}