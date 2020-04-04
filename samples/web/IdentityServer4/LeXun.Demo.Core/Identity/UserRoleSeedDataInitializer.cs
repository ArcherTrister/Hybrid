using Hybrid.Entity;
using Hybrid.Exceptions;
using Hybrid.Identity;

using LeXun.Demo.Identity.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Linq;
using System.Linq.Expressions;

namespace LeXun.Demo.Identity
{
    public class UserRoleSeedDataInitializer : SeedDataInitializerBase<UserRole, int>
    {
        private readonly IServiceProvider _rootProvider;

        /// <summary>
        /// 初始化一个<see cref="SeedDataInitializerBase{TEntity, TKey}"/>类型的新实例
        /// </summary>
        public UserRoleSeedDataInitializer(IServiceProvider rootProvider)
            : base(rootProvider)
        {
            _rootProvider = rootProvider;
        }

        /// <summary>
        /// 重写以提供要初始化的种子数据
        /// </summary>
        /// <returns></returns>
        protected override UserRole[] SyncSeedData()
        {
            return new[]
            {
                new UserRole() { RoleId = 1, UserId = 1 }
            };
        }

        /// <summary>
        /// 重写以提供判断某个实体是否存在的表达式
        /// </summary>
        /// <param name="entity">要判断的实体</param>
        /// <returns></returns>
        protected override Expression<Func<UserRole, bool>> ExistingExpression(UserRole entity)
        {
            return m => m.UserId == entity.UserId && m.RoleId == entity.RoleId;
        }

        /// <summary>
        /// 将种子数据初始化到数据库
        /// </summary>
        /// <param name="entities"></param>
        protected override void SyncToDatabase(UserRole[] entities)
        {
            if (entities?.Length > 0)
            {
                _rootProvider.BeginUnitOfWorkTransaction(provider =>
                {
                    UserRoleManager<User> userManager = provider.GetService<UserManager<User>>();
                    foreach (User user in entities)
                    {
                        if (userManager.Users.Any(ExistingExpression(user)))
                        {
                            continue;
                        }
                        IdentityResult result = userManager.CreateAsync(user).Result;
                        if (!result.Succeeded)
                        {
                            throw new HybridException($"进行用户角色种子数据“{user.UserName}”同步时出错：{result.ErrorMessage()}");
                        }
                    }
                },
                    true);
            }
        }
    }
}
