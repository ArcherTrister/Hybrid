// -----------------------------------------------------------------------
//  <copyright file="StreamExtensions.cs" company="com.esoftor">
//      Copyright © 2019-2020 ESoftor. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-06-12 18:27</last-date>
// -----------------------------------------------------------------------

using System.IO;
using System.Text;

namespace ESoftor.Extensions
{
    /// <summary>
    /// Stream扩展方法
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// 把<see cref="Stream"/>转换为<see cref="string"/>
        /// </summary>
        public static string ToString2(this Stream stream, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            using (StreamReader reader = new StreamReader(stream, encoding))
            {
                return reader.ReadToEnd();
            }
        }
    }
}