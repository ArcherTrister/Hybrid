// -----------------------------------------------------------------------
//  <copyright file="EntityRoleInputDtoBase.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Data;
using ESoftor.Domain.Entities;
using ESoftor.Filter;
using ESoftor.Security;

using System;
using System.ComponentModel;

namespace ESoftor.Permission.Security.Dtos
{
    /// <summary>
    /// 实体角色输入DTO基类
    /// </summary>
    /// <typeparam name="TRoleKey">角色编号类型</typeparam>
    public abstract class EntityRoleInputDtoBase<TRoleKey> : IInputDto<Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="EntityRoleInputDtoBase{TRoleKey}"/>类型的新实例
        /// </summary>
        protected EntityRoleInputDtoBase()
        {
            FilterGroup = new FilterGroup();
        }

        private Guid _id;

        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        [DisplayName("编号")]
        public Guid Id
        {
            get { return _id; }
            set
            {
                if (value == Guid.Empty)
                {
                    value = CombGuid.NewGuid();
                }
                _id = value;
            }
        }

        /// <summary>
        /// 获取或设置 角色编号
        /// </summary>
        [DisplayName("角色编号")]
        public TRoleKey RoleId { get; set; }

        /// <summary>
        /// 获取或设置 数据编号
        /// </summary>
        [DisplayName("数据编号")]
        public Guid EntityId { get; set; }

        /// <summary>
        /// 获取或设置 数据权限操作
        /// </summary>
        [DisplayName("数据权限操作")]
        public DataAuthOperation Operation { get; set; }

        /// <summary>
        /// 获取或设置 过滤条件组
        /// </summary>
        [DisplayName("数据筛选条件组")]
        public FilterGroup FilterGroup { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }
    }
}