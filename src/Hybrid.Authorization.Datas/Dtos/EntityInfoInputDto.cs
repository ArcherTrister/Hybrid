// -----------------------------------------------------------------------
//  <copyright file="EntityInfoInputDto.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2017 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-11-15 17:26</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.EntityInfos;
using Hybrid.Mapping;

namespace Hybrid.Authorization.Dtos
{
    /// <summary>
    /// 输入DTO：实体信息
    /// </summary>
    [MapTo(typeof(EntityInfo))]
    public class EntityInfoInputDto : EntityInfoInputDtoBase
    { }
}