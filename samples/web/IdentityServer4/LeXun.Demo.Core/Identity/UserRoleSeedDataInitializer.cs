using Hybrid.Dependency;
using Hybrid.Entity;
using Hybrid.Exceptions;
using Hybrid.Identity;

using LeXun.Demo.Identity.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LeXun.Demo.Identity
{
    [Dependency(ServiceLifetime.Singleton)]
    public class UserRoleSeedDataInitializer : SeedDataInitializerBase<UserRole, Guid>
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

        public override int Order => 10;

        /// <summary>
        /// 重写以提供要初始化的种子数据
        /// </summary>
        /// <returns></returns>
        protected override UserRole[] SeedData()
        {
            return null;
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
            _rootProvider.BeginUnitOfWorkTransaction(provider =>
            {
                UserManager<User> userManager = provider.GetService<UserManager<User>>();
                RoleManager<Role> roleManager = provider.GetService<RoleManager<Role>>();

                User user = userManager.Users.OrderBy(p => p.CreatedTime).FirstOrDefault();

                List<string> roles = roleManager.Roles.Select(p => p.Name).ToList();

                if (user != null && roles.Any())
                {
                    var userRoles = userManager.GetRolesAsync(user).Result;
                    var newRoles = roles.Except(userRoles).ToList();
                    if (newRoles.Any())
                    {
                        IdentityResult result = userManager.AddToRolesAsync(user, newRoles).Result;

                        if (!result.Succeeded)
                        {
                            throw new HybridException($"进行用户角色种子数据“{user.UserName}”同步时出错：{result.ErrorMessage()}");
                        }
                    }
                }
            },
                true);
        }
    }
}
