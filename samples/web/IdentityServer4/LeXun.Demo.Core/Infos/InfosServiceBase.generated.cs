// -----------------------------------------------------------------------
// <auto-generated>
//    此代码由代码生成器生成。
//    手动更改此文件可能导致应用程序出现意外的行为。
//    如果重新生成代码，对此文件的任何修改都会丢失。
//    如果需要扩展此类，请在控制器类型 InfosService 进行继承重写
// </auto-generated>
//
//  <copyright file="IInfosServiceBase.generated.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 LeXun. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;

using Hybrid.Core.Systems;
using Hybrid.Data;
using Hybrid.Entity;
using Hybrid.EventBuses;
using Hybrid.Extensions;
using Hybrid.Identity;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using LeXun.Demo.Infos.Dtos;
using LeXun.Demo.Infos.Entities;


namespace LeXun.Demo.Infos
{
    /// <summary>
    /// 业务实现基类：信息模块
    /// </summary>
    public abstract partial class InfosServiceBase : IInfosContract
    {
        /// <summary>
        /// 初始化一个<see cref="InfosService"/>类型的新实例
        /// </summary>
        protected InfosServiceBase(IServiceProvider provider)
        {
            ServiceProvider = provider;
            Logger = provider.GetLogger(GetType());
        }
    
        #region 属性

        /// <summary>
        /// 获取或设置 服务提供者对象
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 获取或设置 日志对象
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// 获取或设置 站内信信息仓储对象
        /// </summary>
        protected IRepository<Message, Guid> MessageRepository => ServiceProvider.GetService<IRepository<Message, Guid>>();
        
        /// <summary>
        /// 获取或设置 站内信接收记录信息仓储对象
        /// </summary>
        protected IRepository<MessageReceive, Guid> MessageReceiveRepository => ServiceProvider.GetService<IRepository<MessageReceive, Guid>>();
        
        /// <summary>
        /// 获取或设置 站内信回复信息仓储对象
        /// </summary>
        protected IRepository<MessageReply, Guid> MessageReplyRepository => ServiceProvider.GetService<IRepository<MessageReply, Guid>>();
        
        /// <summary>
        /// 获取 事件总线
        /// </summary>
        protected IEventBus EventBus => ServiceProvider.GetService<IEventBus>();

        /// <summary>
        /// 获取 设置存储对象
        /// </summary>
        protected IKeyValueStore KeyValueStore => ServiceProvider.GetService<IKeyValueStore>();

        #endregion
    }
}
