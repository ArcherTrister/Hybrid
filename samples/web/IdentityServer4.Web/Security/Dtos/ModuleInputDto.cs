// -----------------------------------------------------------------------
//  <copyright file="ModuleInputDto.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using ESoftor.Mapping;
using ESoftor.Permission.Security.Dtos;
using ESoftor.Web.Security.Entities;

using System;

namespace ESoftor.Web.Security.Dtos
{
    /// <summary>
    /// 输入DTO：模块信息
    /// </summary>
    [MapTo(typeof(Module))]
    public class ModuleInputDto : ModuleInputDtoBase<Guid>
    { }
}