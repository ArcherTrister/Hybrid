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
    public class UserSeedDataInitializer : SeedDataInitializerBase<User, int>
    {
        private readonly IServiceProvider _rootProvider;

        /// <summary>
        /// 初始化一个<see cref="SeedDataInitializerBase{TEntity, TKey}"/>类型的新实例
        /// </summary>
        public UserSeedDataInitializer(IServiceProvider rootProvider)
            : base(rootProvider)
        {
            _rootProvider = rootProvider;
        }

        /// <summary>
        /// 重写以提供要初始化的种子数据
        /// </summary>
        /// <returns></returns>
        protected override User[] SeedData()
        {
            return new[]
            {
                new User() { UserName = "admin" }
            };
        }

        /// <summary>
        /// 重写以提供判断某个实体是否存在的表达式
        /// </summary>
        /// <param name="entity">要判断的实体</param>
        /// <returns></returns>
        protected override Expression<Func<User, bool>> ExistingExpression(User entity)
        {
            return m => m.UserName == entity.UserName;
        }

        /// <summary>
        /// 将种子数据初始化到数据库
        /// </summary>
        /// <param name="entities"></param>
        protected override void SyncToDatabase(User[] entities)
        {
            if (entities.Any())
            {
                _rootProvider.BeginUnitOfWorkTransaction(provider =>
                {
                    UserManager<User> userManager = provider.GetService<UserManager<User>>();
                    RoleManager<Role> roleManager = provider.GetService<RoleManager<Role>>();
                    foreach (User user in entities)
                    {
                        if (userManager.Users.Any(ExistingExpression(user)))
                        {
                            continue;
                        }
                        IdentityResult result = userManager.CreateAsync(user).Result;
                        Role role = roleManager.Roles.GroupBy(p=>p.CreatedTime).Select(p=>p.OrderBy(e=>e.CreatedTime).FirstOrDefault()).FirstOrDefault();
                        IdentityResult result = userManager.AddToRoleAsync(user, role.Name).Result;
                        if (!result.Succeeded)
                        {
                            throw new HybridException($"进行用户种子数据“{user.UserName}”同步时出错：{result.ErrorMessage()}");
                        }
                    }
                },
                    true);
            }
        }
    }
}
