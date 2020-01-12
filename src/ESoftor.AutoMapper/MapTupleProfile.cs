// -----------------------------------------------------------------------
//  <copyright file="MapAttributeProfile.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-03-07 21:20</last-date>
// -----------------------------------------------------------------------

using AutoMapper;

using ESoftor.Collections;
using ESoftor.Dependency;
using ESoftor.Mapping;
using ESoftor.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;

namespace ESoftor.AutoMapper
{
    /// <summary>
    /// 创建源类型与目标类型的配对
    /// </summary>
    [Dependency(ServiceLifetime.Singleton)]
    public class MapTupleProfile : Profile, IMapTuple
    {
        private readonly IMapFromAttributeTypeFinder _mapFromAttributeTypeFinder;
        private readonly IMapToAttributeTypeFinder _mapToAttributeTypeFinder;
        private readonly ILogger<MapTupleProfile> _logger;

        /// <summary>
        /// 初始化一个<see cref="MapTupleProfile"/>类型的新实例
        /// </summary>
        public MapTupleProfile(
            IMapFromAttributeTypeFinder mapFromAttributeTypeFinder,
            IMapToAttributeTypeFinder mapToAttributeTypeFinder,
            ILoggerFactory loggerFactory)
        {
            _mapFromAttributeTypeFinder = mapFromAttributeTypeFinder;
            _mapToAttributeTypeFinder = mapToAttributeTypeFinder;
            _logger = loggerFactory.CreateLogger<MapTupleProfile>();
        }

        /// <summary>
        /// 执行对象映射构造
        /// </summary>
        public void CreateMap()
        {
            List<(Type Source, Type Target)> tuples = new List<(Type Source, Type Target)>();

            Type[] types = _mapFromAttributeTypeFinder.FindAll(true);
            foreach (Type targetType in types)
            {
                MapFromAttribute attribute = targetType.GetAttribute<MapFromAttribute>(true);
                foreach (Type sourceType in attribute.SourceTypes)
                {
                    var tuple = ValueTuple.Create(sourceType, targetType);
                    tuples.AddIfNotExist(tuple);
                }
            }

            types = _mapToAttributeTypeFinder.FindAll(true);
            foreach (Type sourceType in types)
            {
                MapToAttribute attribute = sourceType.GetAttribute<MapToAttribute>(true);
                foreach (Type targetType in attribute.TargetTypes)
                {
                    var tuple = ValueTuple.Create(sourceType, targetType);
                    tuples.AddIfNotExist(tuple);
                }
            }

            foreach ((Type Source, Type Target) tuple in tuples)
            {
                CreateMap(tuple.Source, tuple.Target);
                _logger.LogDebug($"创建“{tuple.Source}”到“{tuple.Target}”的对象映射关系");
            }
            _logger.LogInformation($"创建{tuples.Count}个对象映射关系");
        }
    }
}