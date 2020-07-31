//------------------------------------------------------------------------------
// <auto-generated>
//    此代码由代码生成器生成。
//    手动更改此文件可能导致应用程序出现意外的行为。
//    如果重新生成代码，对此文件的任何修改都会丢失。
//    如果需要扩展此类：可遵守如下规则进行扩展：
//      1.横向扩展：如需添加额外的属性，可新建文件“Message.cs”的分部类“public partial class Message”}添加属性
// </auto-generated>
//
//  <copyright file="Message.generated.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Liuliu. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Hybrid.Entity;

using Liuliu.Demo.Identity.Entities;

namespace Liuliu.Demo.Infos.Entities
{
    /// <summary>
    /// 实体类：站内信信息
    /// </summary>
    [Description("站内信信息")]
    public partial class Message : EntityBase<Guid>, ILockable, ISoftDeletable, ICreatedTime
    {
        /// <summary>
        /// 获取或设置 标题
        /// </summary>
        [DisplayName("标题"), Required]
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置 内容
        /// </summary>
        [DisplayName("内容"), Required]
        public string Content { get; set; }

        /// <summary>
        /// 获取或设置 消息类型
        /// </summary>
        [DisplayName("消息类型")]
        public MessageType MessageType { get; set; }

        /// <summary>
        /// 获取或设置 新回复数
        /// </summary>
        [DisplayName("新回复数")]
        public int NewReplyCount { get; set; }

        /// <summary>
        /// 获取或设置 是否发送
        /// </summary>
        [DisplayName("是否发送")]
        public bool IsSended { get; set; }

        /// <summary>
        /// 获取或设置 是否允许回复
        /// </summary>
        [DisplayName("是否允许回复")]
        public bool CanReply { get; set; }

        /// <summary>
        /// 获取或设置 生效时间
        /// </summary>
        [DisplayName("生效时间")]
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        [DisplayName("过期时间")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 删除时间
        /// </summary>
        [DisplayName("删除时间")]
        public DateTime? DeletedTime { get; set; }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 发送人编号
        /// </summary>
        [DisplayName("发送人编号"), UserFlag]
        public int SenderId { get; set; }

        /// <summary>
        /// 获取或设置 发送人
        /// </summary>
        [DisplayName("发送人")]
        public virtual User Sender { get; set; }

        /// <summary>
        /// 获取或设置 接收角色集合，用于公共消息
        /// </summary>
        [DisplayName("接收角色集合，用于公共消息")]
        public virtual ICollection<Role> PublicRoles { get; set; }

        /// <summary>
        /// 获取或设置 接收用户集合，用于私人消息
        /// </summary>
        [DisplayName("接收用户集合，用于私人消息")]
        public virtual ICollection<User> Recipients { get; set; }

        /// <summary>
        /// 获取或设置 消息接收记录集合
        /// </summary>
        [DisplayName("消息接收记录集合")]
        public virtual ICollection<MessageReceive> Receives { get; set; }

        /// <summary>
        /// 获取或设置 回复的消息集合
        /// </summary>
        [DisplayName("回复的消息集合")]
        public virtual ICollection<MessageReply> Replies { get; set; }

    }
}
