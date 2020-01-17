// -----------------------------------------------------------------------
//  <copyright file="NameValueDto.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 15:10</last-date>
// -----------------------------------------------------------------------

using System;

namespace Hybrid.Application.Services.Dtos
{
    /// <summary>
    /// Can be used to send/receive Name/Value (or Key/Value) pairs.
    /// </summary>
    [Serializable]
    public class NameValueDto<T> : NameValue<T>
    {
        /// <summary>
        /// Creates a new <see cref="NameValueDto{T}"/>.
        /// </summary>
        public NameValueDto()
        {
        }

        /// <summary>
        /// Creates a new <see cref="NameValueDto{T}"/>.
        /// </summary>
        public NameValueDto(string name, T value)
            : base(name, value)
        {
        }

        /// <summary>
        /// Creates a new <see cref="NameValueDto{T}"/>.
        /// </summary>
        /// <param name="nameValue">A <see cref="NameValue{T}"/> object to get it's name and value</param>
        public NameValueDto(NameValue<T> nameValue)
            : this(nameValue.Name, nameValue.Value)
        {
        }
    }
}