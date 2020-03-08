// -----------------------------------------------------------------------
//  <copyright file="EntityInfoInputDto.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Authorization.EntityInfos;
using Hybrid.Mapping;

namespace Hybrid.Zero.Authorization.Dtos
{
    /// <summary>
    /// 输入DTO：实体信息
    /// </summary>
    [MapTo(typeof(EntityInfo))]
    public class EntityInfoInputDto : EntityInfoInputDtoBase
    { }
}