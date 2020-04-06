// -----------------------------------------------------------------------
// <auto-generated>
//    此代码由代码生成器生成。
//    手动更改此文件可能导致应用程序出现意外的行为。
//    如果重新生成代码，对此文件的任何修改都会丢失。
//    如果需要扩展此类：可遵守如下规则进行扩展：
//      1.横向扩展：如需添加额外的属性，可新建文件“MessageReplyInputDto.cs”的分部类“public partial class MessageReplyInputDto”}添加属性
// </auto-generated>
//
//  <copyright file="MessageReplyInputDto.generated.cs" company="Hybrid开源团队">
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
using Hybrid.Mapping;

using Liuliu.Demo.Infos.Entities;


namespace Liuliu.Demo.Infos.Dtos
{
    /// <summary>
    /// 输入DTO：站内信回复信息
    /// </summary>
    [MapTo(typeof(MessageReply))]
    [Description("站内信回复信息")]
    public partial class MessageReplyInputDto : IInputDto<Guid>
    {
        /// <summary>
        /// 获取或设置 编号
        /// </summary>
        [DisplayName("编号")]
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 消息内容
        /// </summary>
        [DisplayName("消息内容"), Required]
        public string Content { get; set; }

        /// <summary>
        /// 获取或设置 是否已读
        /// </summary>
        [DisplayName("是否已读")]
        public bool IsRead { get; set; }

        /// <summary>
        /// 获取或设置  消息回复人编号
        /// </summary>
        [DisplayName(" 消息回复人编号")]
        public int UserId { get; set; }

        /// <summary>
        /// 获取或设置 回复的主消息，当回复主消息时有效
        /// </summary>
        [DisplayName("回复的主消息，当回复主消息时有效")]
        public Guid ParentMessageId { get; set; }

        /// <summary>
        /// 获取或设置 回复的回复消息，当回复回复消息时有效
        /// </summary>
        [DisplayName("回复的回复消息，当回复回复消息时有效")]
        public Guid ParentReplyId { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }

    }
}
