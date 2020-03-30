// -----------------------------------------------------------------------
//  <copyright file="ModuleSeedDataInitializer.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-03-07 1:01</last-date>
// -----------------------------------------------------------------------

using Hybrid.Entity;

using LeXun.Demo.Authorization.Entities;

using System;
using System.Linq.Expressions;

namespace LeXun.Demo.Authorization
{
    public class ModuleSeedDataInitializer : SeedDataInitializerBase<Module, int>
    {
        /// <summary>
        /// 初始化一个<see cref="SeedDataInitializerBase{TEntity, TKey}"/>类型的新实例
        /// </summary>
        public ModuleSeedDataInitializer(IServiceProvider rootProvider)
            : base(rootProvider)
        { }

        /// <summary>
        /// 重写以提供要初始化的种子数据
        /// </summary>
        /// <returns></returns>
        protected override Module[] SeedData()
        {
            return new[]
            {
                new Module() { Name = "根节点", Remark = "系统根节点", Code = "Root", OrderCode = 1, TreePathString = "$1$" },
            };
        }

        /// <summary>
        /// 重写以提供判断某个实体是否存在的表达式
        /// </summary>
        /// <param name="entity">要判断的实体</param>
        /// <returns></returns>
        protected override Expression<Func<Module, bool>> ExistingExpression(Module entity)
        {
            return m => m.TreePathString == entity.TreePathString;
        }
    }
}