// -----------------------------------------------------------------------
//  <copyright file="StreamExtensions.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-06-12 18:27</last-date>
// -----------------------------------------------------------------------

using System.IO;
using System.Text;


namespace Hybrid.Extensions
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

        /// <summary>
        /// 流转换字节数组
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] GetAllBytes(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}