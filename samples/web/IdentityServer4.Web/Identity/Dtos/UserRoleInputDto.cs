// -----------------------------------------------------------------------
//  <copyright file="UserRoleInputDto.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Mapping;
using Hybrid.Web.Identity.Entities;
using Hybrid.Zero.Identity;
using Hybrid.Zero.Identity.Dtos;
using System;

namespace Hybrid.Web.Identity.Dto
{
    /// <summary>
    /// 输入DTO：用户角色信息
    /// </summary>
    [MapTo(typeof(UserRole))]
    public class UserRoleInputDto : UserRoleInputDtoBase<Guid, Guid>
    { }
}