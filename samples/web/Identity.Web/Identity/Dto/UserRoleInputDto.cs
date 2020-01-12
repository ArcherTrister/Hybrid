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
