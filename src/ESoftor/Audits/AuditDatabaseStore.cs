// -----------------------------------------------------------------------
//  <copyright file="AuditDatabaseStore.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>http://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 4:31</last-date>
// -----------------------------------------------------------------------

using ESoftor.Data;
using ESoftor.Domain.Entities.Auditing;
using ESoftor.Domain.Repositories;
using ESoftor.Mapping;
using ESoftor.Net;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ESoftor.Audits
{
    /// <summary>
    /// 数据库审计存储
    /// </summary>
    public class AuditDatabaseStore : IAuditStore
    {
        private readonly IRepository<AuditOperation, Guid> _operationRepository;
        //private readonly ILogger<AuditDatabaseStore> _logger;

        /// <summary>
        /// 初始化一个<see cref="AuditDatabaseStore"/>类型的新实例
        /// </summary>
        public AuditDatabaseStore(IRepository<AuditOperation, Guid> operationRepository)
        {
            _operationRepository = operationRepository;
            //_logger = loggerFactory.CreateLogger<AuditDatabaseStore>();, ILoggerFactory loggerFactory
        }

        /// <summary>
        /// 设置保存审计数据
        /// </summary>
        /// <param name="operationEntry">操作审计数据</param>
        public void Save(AuditOperationEntry operationEntry)
        {
            AuditOperation operation = BuildOperation(operationEntry);
            _operationRepository.Insert(operation);
        }

        /// <summary>
        /// 异步保存实体审计数据
        /// </summary>
        /// <param name="operationEntry">操作审计数据</param>
        /// <param name="cancelToken">异步取消标识</param>
        /// <returns></returns>
        public async Task SaveAsync(AuditOperationEntry operationEntry, CancellationToken cancelToken = default(CancellationToken))
        {
            AuditOperation operation = BuildOperation(operationEntry);
            await _operationRepository.InsertAsync(operation);
        }

        private static AuditOperation BuildOperation(AuditOperationEntry operationEntry)
        {
            AuditOperation operation = operationEntry.MapTo<AuditOperation>();
            if (operationEntry.UserAgent != null)
            {
                UserAgent userAgent = new UserAgent(operationEntry.UserAgent);
                operation.OperationSystem = userAgent.GetSystem();
                operation.Browser = userAgent.GetBrowser();
            }
            operation.Elapsed = (int)operationEntry.EndedTime.Subtract(operationEntry.CreatedTime).TotalMilliseconds;
            if (operation.ResultType == AjaxResultType.Success)
            {
                foreach (AuditEntityEntry entityEntry in operationEntry.EntityEntries)
                {
                    AuditEntity entity = entityEntry.MapTo<AuditEntity>();
                    operation.AuditEntities.Add(entity);
                    foreach (AuditPropertyEntry propertyEntry in entityEntry.PropertyEntries)
                    {
                        AuditProperty property = propertyEntry.MapTo<AuditProperty>();
                        entity.Properties.Add(property);
                    }
                }
            }
            return operation;
        }

        //public bool ShouldSaveAudit(MethodInfo methodInfo, bool defaultValue = false)
        //{
        //    throw new NotImplementedException();
        //}

        //public AuditOperationEntry CreateAuditInfo(Type type, MethodInfo method, object[] arguments)
        //{
        //    return CreateAuditInfo(type, method, CreateArgumentsDictionary(method, arguments));
        //}

        //public AuditOperationEntry CreateAuditInfo(Type type, MethodInfo method, IDictionary<string, object> arguments)
        //{
        //    var auditInfo = new AuditOperationEntry
        //    {
        //        TenantId = AbpSession.TenantId,
        //        UserId = AbpSession.UserId,
        //        ServiceName = type != null
        //            ? type.FullName
        //            : "",
        //        FunctionName = method.Name,
        //        Parameters = ConvertArgumentsToJson(arguments),
        //        ExecutionTime = DateTime.Now
        //    };

        //    try
        //    {
        //        _auditInfoProvider.Fill(auditInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogWarning(ex.ToString(), ex);
        //    }

        //    return auditInfo;
        //}

        //private string ConvertArgumentsToJson(IDictionary<string, object> arguments)
        //{
        //    try
        //    {
        //        if (arguments.IsNullOrEmpty())
        //        {
        //            return "{}";
        //        }

        //        var dictionary = new Dictionary<string, object>();

        //        foreach (var argument in arguments)
        //        {
        //            if (argument.Value != null && _configuration.IgnoredTypes.Any(t => t.IsInstanceOfType(argument.Value)))
        //            {
        //                dictionary[argument.Key] = null;
        //            }
        //            else
        //            {
        //                dictionary[argument.Key] = argument.Value;
        //            }
        //        }

        //        return AuditingHelper.Serialize(dictionary);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogWarning(ex.ToString(), ex);
        //        return "{}";
        //    }
        //}

        //private static Dictionary<string, object> CreateArgumentsDictionary(MethodInfo method, object[] arguments)
        //{
        //    var parameters = method.GetParameters();
        //    var dictionary = new Dictionary<string, object>();

        //    for (var i = 0; i < parameters.Length; i++)
        //    {
        //        dictionary[parameters[i].Name] = arguments[i];
        //    }

        //    return dictionary;
        //}
    }
}