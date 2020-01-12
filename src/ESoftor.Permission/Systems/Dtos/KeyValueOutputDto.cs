// -----------------------------------------------------------------------
//  <copyright file="KeyValueOutputDto.cs" company="com.esoftor">
//      Copyright ? 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Systems;
using ESoftor.Domain.Entities;
using ESoftor.Mapping;

using System;
using System.ComponentModel;

namespace ESoftor.Permission.Systems.Dtos
{
    /// <summary>
    /// 输出DTO:键值数据
    /// </summary>
    [MapFrom(typeof(KeyValue))]
    public class KeyValueOutputDto : IOutputDto, IDataAuthEnabled
    {
        /// <summary>
        /// 获取或设置 编号
        /// </summary>
        [DisplayName("编号")]
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 数据值JSON
        /// </summary>
        [DisplayName("数据值JSON")]
        public string ValueJson { get; set; }

        /// <summary>
        /// 获取或设置 数据值类型名
        /// </summary>
        [DisplayName("数据值类型名")]
        public string ValueType { get; set; }

        /// <summary>
        /// 获取或设置 数据键名
        /// </summary>
        [DisplayName("数据键名")]
        public string Key { get; set; }

        /// <summary>
        /// 获取或设置 数据值
        /// </summary>
        [DisplayName("数据值")]
        public object Value { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 是否可更新的数据权限状态
        /// </summary>
        public bool Updatable { get; set; }

        /// <summary>
        /// 获取或设置 是否可删除的数据权限状态
        /// </summary>
        public bool Deletable { get; set; }
    }
}