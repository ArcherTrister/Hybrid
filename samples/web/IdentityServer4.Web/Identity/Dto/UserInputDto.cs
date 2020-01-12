// -----------------------------------------------------------------------
//  <copyright file="UserInputDto.cs" company="cn.lxking">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using ESoftor.Mapping;
using ESoftor.Permission.Identity.Dtos;
using ESoftor.Web.Identity.Entity;

using System;

namespace ESoftor.Web.Identity.Dtos
{
    /// <summary>
    /// 输入DTO：用户信息
    /// </summary>
    [MapTo(typeof(User))]
    public class UserInputDto : UserInputDtoBase<Guid>
    { }
}