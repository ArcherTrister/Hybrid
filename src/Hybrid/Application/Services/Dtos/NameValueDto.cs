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