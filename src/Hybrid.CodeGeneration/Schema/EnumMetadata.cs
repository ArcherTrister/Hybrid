// -----------------------------------------------------------------------
//  <copyright file="EnumMetadata.cs" company="Hybrid��Դ�Ŷ�">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-06 12:44</last-date>
// -----------------------------------------------------------------------

using System;

using Hybrid.Extensions;


namespace Hybrid.CodeGeneration.Schema
{
    /// <summary>
    /// ö������Ԫ����
    /// </summary>
    public class EnumMetadata
    {
        /// <summary>
        /// ��ʼ��һ��<see cref="EnumMetadata"/>���͵���ʵ��
        /// </summary>
        public EnumMetadata()
        { }

        /// <summary>
        /// ��ʼ��һ��<see cref="EnumMetadata"/>���͵���ʵ��
        /// </summary>
        public EnumMetadata(Enum enumItem)
        {
            if (enumItem == null)
            {
                return;
            }
            Value = enumItem.CastTo<int>();
            Name = enumItem.ToString();
            Display = enumItem.ToDescription();
        }

        /// <summary>
        /// ��ȡ������ ö��ֵ����ֵ
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// ��ȡ������ ö������
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ��ȡ������ ��ʾ����
        /// </summary>
        public string Display { get; set; }
    }
}