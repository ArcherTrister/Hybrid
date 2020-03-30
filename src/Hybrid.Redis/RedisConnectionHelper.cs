// -----------------------------------------------------------------------
//  <copyright file="RedisConnectionHelper.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2020-03-26 2:24</last-date>
// -----------------------------------------------------------------------

using StackExchange.Redis;

using System.Collections.Generic;
using System.Threading;

namespace Hybrid.Redis
{
    /// <summary>
    /// Redis连接帮助类
    /// </summary>
    public class RedisConnectionHelper
    {
        private static readonly IDictionary<string, ConnectionMultiplexer> ConnectionCache = new Dictionary<string, ConnectionMultiplexer>();
        private static readonly SemaphoreSlim ConnectionLock = new SemaphoreSlim(1, 1);

        /// <summary>
        /// 连接到指定服务器
        /// </summary>
        private static ConnectionMultiplexer Connect(string host)
        {
            ConnectionLock.Wait();
            try
            {
                if (ConnectionCache.TryGetValue(host, out ConnectionMultiplexer connection) && connection.IsConnected)
                {
                    return connection;
                }

                connection = ConnectionMultiplexer.Connect(host);
                ConnectionCache[host] = connection;
                return connection;
            }
            finally
            {
                ConnectionLock.Release();
            }
        }
    }
}