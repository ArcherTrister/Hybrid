﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hybrid.Application.Services.Dtos
{
    /// <summary>
    /// Can be used to store Name/Value (or Key/Value) pairs.
    /// </summary>
    [Serializable]
    public class NameValue<T>
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Creates a new <see cref="NameValue{T}"/>.
        /// </summary>
        public NameValue()
        {
        }

        /// <summary>
        /// Creates a new <see cref="NameValue{T}"/>.
        /// </summary>
        public NameValue(string name, T value)
        {
            Name = name;
            Value = value;
        }
    }
}
