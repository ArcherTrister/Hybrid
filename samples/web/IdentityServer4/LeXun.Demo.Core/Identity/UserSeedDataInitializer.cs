using Hybrid.Data;
using Hybrid.Dependency;
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
    [Dependency(ServiceLifetime.Singleton)]
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
                new User()
                {
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    NickName = "SuperAdmin",
                    Gender = GenderType.Male,
                    Email = "Admin@example.com",
                    NormalizeEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    //Hybrid123
                    PasswordHash = "AQAAAAEAACcQAAAAEBbgICc9joXdXMXQlYvlzPCEz67YTa9hINuuqmrP6BJ6LyO+Z7enJT4IudfIRmbIJQ==",
                    Avatar = null,
                    SecurityStamp = "RRYXXETXCDKPXE6QPNDGLMCYNBA2ZF4P",
                    ConcurrencyStamp = "e50ea89e-c966-4ade-8fe4-6fe94de83777",
                    PhoneNumber = "18100000000",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    IsSystem = true,
                    IsLocked = false,
                    Remark = null
                }
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
                    foreach (User user in entities)
                    {
                        if (userManager.Users.Any(ExistingExpression(user)))
                        {
                            continue;
                        }
                        IdentityResult result = userManager.CreateAsync(user).Result;
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
