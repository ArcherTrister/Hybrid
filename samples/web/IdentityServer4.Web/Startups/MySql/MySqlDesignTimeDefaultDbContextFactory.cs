﻿// -----------------------------------------------------------------------
//  <copyright file="MySqlDesignTimeDefaultDbContextFactory.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-27 4:50</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Options;
using Hybrid.Data;
using Hybrid.EntityFrameworkCore;
using Hybrid.EntityFrameworkCore.Defaults;
using Hybrid.Exceptions;
using Hybrid.Extensions;
using Hybrid.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Reflection;

namespace Hybrid.Web.Startups.MySql
{
    public class MySqlDesignTimeDefaultDbContextFactory : DesignTimeDbContextFactoryBase<DefaultDbContext>
    {
        private readonly IServiceProvider _serviceProvider;

        public MySqlDesignTimeDefaultDbContextFactory()
        { }

        public MySqlDesignTimeDefaultDbContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override string GetConnectionString()
        {
            if (_serviceProvider == null)
            {
                IConfiguration configuration = Singleton<IConfiguration>.Instance;
                string str = configuration["Hybrid:DbContexts:MySql:ConnectionString"];
                return str;
            }
            HybridOptions options = _serviceProvider.GetHybridOptions();
            HybridDbContextOptions contextOptions = options.GetDbContextOptions(typeof(DefaultDbContext));
            if (contextOptions == null)
            {
                throw new HybridException($"上下文“{typeof(DefaultDbContext)}”的配置信息不存在");
            }
            return contextOptions.ConnectionString;
        }

        public override IEntityManager GetEntityManager()
        {
            if (_serviceProvider != null)
            {
                return _serviceProvider.GetService<IEntityManager>();
            }
            IEntityConfigurationTypeFinder typeFinder = new EntityConfigurationTypeFinder(new AppDomainAllAssemblyFinder());
            IEntityManager entityManager = new EntityManager(typeFinder);
            entityManager.Initialize();
            return entityManager;
        }

        public override bool LazyLoadingProxiesEnabled()
        {
            if (_serviceProvider == null)
            {
                IConfiguration configuration = Singleton<IConfiguration>.Instance;
                return configuration["Hybrid:DbContexts:MySql:LazyLoadingProxiesEnabled"].CastTo(false);
            }
            HybridOptions options = _serviceProvider.GetHybridOptions();
            HybridDbContextOptions contextOptions = options.GetDbContextOptions(typeof(DefaultDbContext));
            if (contextOptions == null)
            {
                throw new HybridException($"上下文“{typeof(DefaultDbContext)}”的配置信息不存在");
            }

            return contextOptions.LazyLoadingProxiesEnabled;
        }

        public override DbContextOptionsBuilder UseSql(DbContextOptionsBuilder builder, string connString)
        {
            string entryAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            Console.WriteLine($"entryAssemblyName: {entryAssemblyName}");
            return builder.UseMySql(connString, b => b.MigrationsAssembly(entryAssemblyName));
        }
    }
}