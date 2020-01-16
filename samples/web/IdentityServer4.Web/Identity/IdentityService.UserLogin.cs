using Hybrid.Data;
using Hybrid.Exceptions;
using Hybrid.Security.Claims;
using Hybrid.Web.Identity.Entity;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hybrid.Web.Identity
{
    public partial class IdentityService
    {
        /// <summary>
        /// 获取 第三方登录用户信息查询数据集
        /// </summary>
        public IQueryable<UserLogin> UserLogins
        {
            get { return _userLoginRepository.QueryAsNoTracking(); }
        }

        /// <summary>
        /// 删除实体信息信息
        /// </summary>
        /// <param name="ids">要删除的实体信息编号</param>
        /// <returns>业务操作结果</returns>
        public Task<OperationResult> DeleteUserLogins(params Guid[] ids)
        {
            return _userLoginRepository.DeleteAsync(ids,
                entity =>
                {
                    Guid userId = _currentUser.Identity.GetUserId<Guid>();
                    if (entity.UserId != userId)
                    {
                        throw new HybridException("要解除的第三方登录绑定不属于当前用户");
                    }

                    var user = _userManager.Users.Where(m => m.Id == userId).Select(m => new { m.PasswordHash, m.NormalizedEmail }).First();
                    if ((string.IsNullOrEmpty(user.PasswordHash) || string.IsNullOrEmpty(user.NormalizedEmail))
                        && _userLoginRepository.QueryAsNoTracking(m => m.UserId == entity.UserId).Count() == 1)
                    {
                        throw new HybridException("当前用户未设置登录密码，并且要解除的第三方登录是唯一登录方式，无法解除");
                    }
                    return Task.FromResult(0);
                });
        }
    }
}