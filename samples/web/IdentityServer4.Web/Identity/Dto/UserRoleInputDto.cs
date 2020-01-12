// -----------------------------------------------------------------------
//  <copyright file="UserRoleInputDto.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Mapping;
using ESoftor.Permission.Identity;
using ESoftor.Web.Identity.Entity;

using System;

namespace ESoftor.Web.Identity.Dto
{
    /// <summary>
    /// 输入DTO：用户角色信息
    /// </summary>
    [MapTo(typeof(UserRole))]
    public class UserRoleInputDto : UserRoleInputDtoBase<Guid, Guid>
    { }
}