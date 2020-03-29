using Hybrid.Entity;

using System;

namespace Hybrid.Application.Services.Dtos
{
    /// <summary>
    /// 定义输入DTO
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    [Serializable]
    public class InputDto<TKey> : IInputDto<TKey>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// Creates a new <see cref="InputDto{TKey}"/> object.
        /// </summary>
        public InputDto()
        {
        }

        /// <summary>
        /// Creates a new <see cref="InputDto{TKey}"/> object.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        public InputDto(TKey id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// 定义输出DTO
    /// </summary>
    [Serializable]
    public class OutputDto : IOutputDto
    {
    }

    /// <summary>
    /// 定义数据权限的更新，删除状态
    /// </summary>
    [Serializable]
    public class DataAuthEnabled : IDataAuthEnabled
    {
        /// <summary>
        /// 获取或设置 是否可更新的数据权限状态
        /// </summary>
        public bool Updatable { get; set; }

        /// <summary>
        /// 获取或设置 是否可删除的数据权限状态
        /// </summary>
        public bool Deletable { get; set; }
    }
}
