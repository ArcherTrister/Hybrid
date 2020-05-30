// -----------------------------------------------------------------------
//  <copyright file="EntityMetadata.cs" company="Hybrid��Դ�Ŷ�">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-06 12:25</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Hybrid.Collections;
using Hybrid.Exceptions;


namespace Hybrid.CodeGeneration.Schema
{
    /// <summary>
    /// ʵ��Ԫ����
    /// </summary>
    public class EntityMetadata
    {
        private ModuleMetadata _module;

        /// <summary>
        /// ��ȡ������ ��������
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ��ȡ������ ������ʾ����
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// ��ȡ������ ��������ȫ��
        /// </summary>
        public string PrimaryKeyTypeFullName { get; set; }

        /// <summary>
        /// ��ȡ������ �Ƿ�����Ȩ�޿���
        /// </summary>
        public bool IsDataAuth { get; set; }

        /// <summary>
        /// ��ȡ������ ����ģ����Ϣ
        /// </summary>
        public ModuleMetadata Module
        {
            get => _module;
            set
            {
                _module = value;
                value.Entities.AddIfNotExist(this);
            }
        }

        /// <summary>
        /// ��ȡ������ ʵ������Ԫ���ݼ���
        /// </summary>
        public ICollection<PropertyMetadata> Properties { get; set; } = new List<PropertyMetadata>();
    }
}