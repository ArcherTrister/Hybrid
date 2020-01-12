// -----------------------------------------------------------------------
//  <copyright file="ModuleOutputDto.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using ESoftor.Domain.Entities;
using ESoftor.Mapping;

using System;
using System.Reflection;

namespace ESoftor.Web.Security.Dtos
{
    /// <summary>
    /// 输入DTO:模块信息
    /// </summary>
    [MapFrom(typeof(Module))]
    public class ModuleOutputDto : IOutputDto
    {
        /// <summary>
        /// 获取或设置 模块编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 模块代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置 节点内排序码
        /// </summary>
        public double OrderCode { get; set; }

        /// <summary>
        /// 获取或设置 父模块编号
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}