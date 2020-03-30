// -----------------------------------------------------------------------
//  <copyright file="EntityHashExtensions.cs" company="Hybrid��Դ�Ŷ�">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-12 10:09</last-date>
// -----------------------------------------------------------------------

using Hybrid.Collections;
using Hybrid.Core.Data;
using Hybrid.Core.Systems;
using Hybrid.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hybrid.Entity
{
    /// <summary>
    /// ʵ��Hash��չ����
    /// </summary>
    public static class EntityHashExtensions
    {
        /// <summary>
        /// ���ָ��ʵ���Hashֵ�������Ƿ���Ҫ�������ݿ�ͬ��
        /// </summary>
        public static bool CheckSyncByHash(this IEnumerable<IEntityHash> entityHashes, IServiceProvider provider, ILogger logger)
        {
            IEntityHash[] hashes = entityHashes as IEntityHash[] ?? entityHashes.ToArray();
            if (hashes.Length == 0)
            {
                return false;
            }
            string hash = hashes.Select(m => m.GetHash()).ExpandAndToString().ToMd5Hash();
            IKeyValueStore store = provider.GetService<IKeyValueStore>();
            string entityType = hashes[0].GetType().FullName;
            string key = $"Hybrid.Initialize.SyncToDatabaseHash-{entityType}";
            IKeyValue keyValue = store.GetKeyValue(key);
            if (keyValue != null && keyValue.Value?.ToString() == hash)
            {
                logger.LogInformation($"{hashes.Length}���������ݡ�{entityType}��������ǩ�� {hash} ���ϴ���ͬ��ȡ�����ݿ�ͬ��");
                return false;
            }

            store.CreateOrUpdateKeyValue(key, hash).GetAwaiter().GetResult();
            logger.LogInformation($"{hashes.Length}���������ݡ�{entityType}��������ǩ�� {hash} ���ϴ� {keyValue?.Value} ��ͬ�����������ݿ�ͬ��");
            return true;
        }

        /// <summary>
        /// ��ȡָ��ʵ���Hashֵ
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        public static string GetHash(this IEntityHash entity)
        {
            Type type = entity.GetType();
            StringBuilder sb = new StringBuilder();
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(m => m.CanWrite && m.Name != "Id"))
            {
                sb.Append(property.GetValue(entity));
            }
            return sb.ToString().ToMd5Hash();
        }
    }
}