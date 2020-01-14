// -----------------------------------------------------------------------
//  <copyright file="TypeMetadataHandler.cs" company="com.esoftor">
//      Copyright ? 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-06 13:44</last-date>
// -----------------------------------------------------------------------

using ESoftor.Domain.Entities;
using ESoftor.Extensions;
using ESoftor.Reflection;

using System;
using System.Linq;

namespace ESoftor.CodeGenerator
{
    /// <summary>
    /// 类型元数据处理器
    /// </summary>
    public class TypeMetadataHandler : ITypeMetadataHandler
    {
        private readonly IEntityTypeFinder _entityTypeFinder;
        private readonly IInputDtoTypeFinder _inputDtoTypeFinder;
        private readonly IOutputDtoTypeFinder _outputDtoTypeFinder;

        /// <summary>
        /// 初始化一个<see cref="TypeMetadataHandler"/>类型的新实例
        /// </summary>
        public TypeMetadataHandler(IEntityTypeFinder entityTypeFinder,
            IInputDtoTypeFinder inputDtoTypeFinder,
            IOutputDtoTypeFinder outputDtoTypeFinder)
        {
            _entityTypeFinder = entityTypeFinder;
            _inputDtoTypeFinder = inputDtoTypeFinder;
            _outputDtoTypeFinder = outputDtoTypeFinder;
        }

        /// <summary>
        /// 获取实体类的元数据
        /// </summary>
        /// <returns>元数据集合</returns>
        public TypeMetadata[] GetEntityTypeMetadatas()
        {
            Type[] entityTypes = _entityTypeFinder.Find(m => !m.HasAttribute<IgnoreGenTypeAttribute>());
            return entityTypes.OrderBy(m => m.FullName).Select(m => new TypeMetadata(m)).ToArray();
        }

        /// <summary>
        /// 获取输入DTO类型的元数据
        /// </summary>
        /// <returns>元数据集合</returns>
        public TypeMetadata[] GetInputDtoMetadatas()
        {
            Type[] inputDtoTypes = _inputDtoTypeFinder.Find(m => !m.HasAttribute<IgnoreGenTypeAttribute>());
            return inputDtoTypes.OrderBy(m => m.FullName).Select(m => new TypeMetadata(m)).ToArray();
        }

        /// <summary>
        /// 获取输出DTO类型的元数据
        /// </summary>
        /// <returns>元数据集合</returns>
        public TypeMetadata[] GetOutputDtoMetadata()
        {
            Type[] outDtoTypes = _outputDtoTypeFinder.Find(m => !m.HasAttribute<IgnoreGenTypeAttribute>());
            return outDtoTypes.OrderBy(m => m.FullName).Select(m => new TypeMetadata(m)).ToArray();
        }

        /// <summary>
        /// 获取指定类型的元数据
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>元数据</returns>
        public TypeMetadata GetTypeMetadata(Type type)
        {
            if (type == null)
            {
                return null;
            }
            return new TypeMetadata(type);
        }
    }
}