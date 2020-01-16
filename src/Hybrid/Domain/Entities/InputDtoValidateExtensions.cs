// -----------------------------------------------------------------------
//  <copyright file="InputDtoValidateExtensions.cs" company="com.esoftor">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 11:44</last-date>
// -----------------------------------------------------------------------

using Hybrid.Data;
using Hybrid.Extensions;
using Hybrid.Reflection;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Hybrid.Domain.Entities
{
    /// <summary>
    /// <see cref="IInputDto{TKey}"/>验证扩展
    /// </summary>
    public static class InputDtoValidateExtensions
    {
        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<PropertyInfo, ValidationAttribute[]>> _dict
            = new ConcurrentDictionary<Type, ConcurrentDictionary<PropertyInfo, ValidationAttribute[]>>();

        /// <summary>
        /// InputDto属性验证
        /// </summary>
        public static void Validate<TKey>(this IEnumerable<IInputDto<TKey>> dtos)
        {
            IInputDto<TKey>[] inputDtos = dtos as IInputDto<TKey>[] ?? dtos.ToArray();
            Check.NotNull(inputDtos, nameof(dtos));
            foreach (IInputDto<TKey> dto in inputDtos)
            {
                Validate(dto);
            }
        }

        /// <summary>
        /// InputDto属性验证
        /// </summary>
        public static void Validate<TKey>(this IInputDto<TKey> dto)
        {
            Check.NotNull(dto, nameof(dto));
            Type type = dto.GetType();
            if (!_dict.TryGetValue(type, out ConcurrentDictionary<PropertyInfo, ValidationAttribute[]> dict))
            {
                PropertyInfo[] properties = type.GetProperties();
                dict = new ConcurrentDictionary<PropertyInfo, ValidationAttribute[]>();
                if (properties.Length == 0)
                {
                    _dict[type] = dict;
                    return;
                }
                foreach (var property in properties)
                {
                    dict[property] = null;
                }
                _dict[type] = dict;
            }

            foreach (PropertyInfo property in dict.Keys)
            {
                if (!dict.TryGetValue(property, out ValidationAttribute[] attributes) || attributes == null)
                {
                    attributes = property.GetAttributes<ValidationAttribute>();
                    dict[property] = attributes;
                }
                if (attributes.Length == 0)
                {
                    continue;
                }
                object value = property.GetValue(dto);
                foreach (ValidationAttribute attribute in attributes)
                {
                    attribute.Validate(value, property.Name);
                }
            }
        }
    }
}