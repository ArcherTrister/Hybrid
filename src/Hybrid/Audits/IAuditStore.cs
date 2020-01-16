// -----------------------------------------------------------------------
//  <copyright file="IAuditStore.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-12-31 1:26</last-date>
// -----------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

namespace Hybrid.Audits
{
    /// <summary>
    /// 定义Audit数据存储功能
    /// 注意：审计操作的记录不能和业务数据操作在同一事务中
    /// </summary>
    public interface IAuditStore
    {
        ///// <summary>
        ///// 是否审计
        ///// </summary>
        ///// <param name="methodInfo"></param>
        ///// <param name="defaultValue"></param>
        ///// <returns></returns>
        //bool ShouldSaveAudit(MethodInfo methodInfo, bool defaultValue = false);

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="method"></param>
        ///// <param name="arguments"></param>
        ///// <returns></returns>
        //AuditOperationEntry CreateAuditInfo(ClaimsPrincipal principal, Type type, MethodInfo method, object[] arguments);

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="method"></param>
        ///// <param name="arguments"></param>
        ///// <returns></returns>
        //AuditOperationEntry CreateAuditInfo(Type type, MethodInfo method, IDictionary<string, object> arguments);

        /// <summary>
        /// 设置保存审计数据
        /// </summary>
        /// <param name="operationEntry">操作审计数据</param>
        void Save(AuditOperationEntry operationEntry);

        /// <summary>
        /// 异步保存实体审计数据
        /// </summary>
        /// <param name="operationEntry">操作审计数据</param>
        /// <param name="cancelToken">异步取消标识</param>
        /// <returns></returns>
        Task SaveAsync(AuditOperationEntry operationEntry, CancellationToken cancelToken = default(CancellationToken));
    }
}