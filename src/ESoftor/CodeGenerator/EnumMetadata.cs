// -----------------------------------------------------------------------
//  <copyright file="EnumMetadata.cs" company="com.esoftor">
//      Copyright ? 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-06 12:44</last-date>
// -----------------------------------------------------------------------

using ESoftor.Extensions;

using System;

namespace ESoftor.CodeGenerator
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
        /// ��ȡ������ ö��ֵ
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// ��ȡ������ ö����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ��ȡ������ ��ʾ����
        /// </summary>
        public string Display { get; set; }
    }
}