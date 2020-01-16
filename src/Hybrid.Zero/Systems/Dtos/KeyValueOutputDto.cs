// -----------------------------------------------------------------------
//  <copyright file="KeyValueOutputDto.cs" company="cn.lxking">
//      Copyright ? 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using Hybrid.Core.Systems;
using Hybrid.Domain.Entities;
using Hybrid.Mapping;

using System;
using System.ComponentModel;

namespace Hybrid.Zero.Systems.Dtos
{
    /// <summary>
    /// ���DTO:��ֵ����
    /// </summary>
    [MapFrom(typeof(KeyValue))]
    public class KeyValueOutputDto : IOutputDto, IDataAuthEnabled
    {
        /// <summary>
        /// ��ȡ������ ���
        /// </summary>
        [DisplayName("���")]
        public Guid Id { get; set; }

        /// <summary>
        /// ��ȡ������ ����ֵJSON
        /// </summary>
        [DisplayName("����ֵJSON")]
        public string ValueJson { get; set; }

        /// <summary>
        /// ��ȡ������ ����ֵ������
        /// </summary>
        [DisplayName("����ֵ������")]
        public string ValueType { get; set; }

        /// <summary>
        /// ��ȡ������ ���ݼ���
        /// </summary>
        [DisplayName("���ݼ���")]
        public string Key { get; set; }

        /// <summary>
        /// ��ȡ������ ����ֵ
        /// </summary>
        [DisplayName("����ֵ")]
        public object Value { get; set; }

        /// <summary>
        /// ��ȡ������ �Ƿ�����
        /// </summary>
        [DisplayName("�Ƿ�����")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// ��ȡ������ �Ƿ�ɸ��µ�����Ȩ��״̬
        /// </summary>
        public bool Updatable { get; set; }

        /// <summary>
        /// ��ȡ������ �Ƿ��ɾ��������Ȩ��״̬
        /// </summary>
        public bool Deletable { get; set; }
    }
}