using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Conmon.Tests
{
    /// <summary>
    /// 本系统简易通用文件加解密工具类，无需密码
    /// </summary>
    public class FileSecurity
    {
        /// <summary>
        /// 文件头数据：'!青才科技!'的十六进制值
        /// </summary>
        private readonly byte[] header = new byte[]
        {
            0x21,
            0xE9, 0x9D, 0x92,
            0xE6, 0x89, 0x8D,
            0xE7, 0xA7, 0x91,
            0xE6, 0x8A, 0x80,
            0x21
        };

        private FileSecurity()
        {
        }

        /// <summary>
        /// 唯一实例
        /// </summary>
        public static FileSecurity Instance => new FileSecurity();

        /// <summary>
        /// 加密文件(覆盖源文件)
        /// </summary>
        /// <param name="file">文件路径</param>
        public void Encrypt(string file)
        {
            /* 加密过程：
             * 1、判断是否已加密，已加密则跳过
             * 2、若未加密，在文件头加入特征码(用于校验加密标识)
             * 3、将文件中每个字节与0xFF做异或运算，保存
             */
            if (IsEncrypted(file))
            {
                return;
            }
            byte[] data = null;
            using (FileStream fs = new FileStream(file, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    byte[] binary = reader.ReadBytes((int)fs.Length);
                    data = MergeBytes(header, XorFF(binary));
                }
            }
            using (FileStream fs = new FileStream(file, FileMode.Open))
            {
                using (BinaryWriter writter = new BinaryWriter(fs))
                {
                    writter.Write(data);
                    writter.Flush();
                }
            }
        }

        /// <summary>
        /// 解密文件，注意使用返回结果后需要释放资源以免造成内存泄露
        /// （不会覆盖源文件）
        /// </summary>
        /// <param name="file">文件路径</param>
        public MemoryStream Descrypt(string file)
        {
            /* 加密过程：
             * 1、判断是否已加密
             * 2、若已加密，在文件头加入特征码(用于校验加密标识)，将文件中每个字节与0xFF做异或运算，保存
             */
            if (IsEncrypted(file))
            {
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        MemoryStream ms = new MemoryStream();
                        byte[] binary = reader.ReadBytes((int)fs.Length);
                        byte[] _binary = XorFF(binary);
                        ms.Write(_binary, header.Length, binary.Length - header.Length);

                        return ms;
                    }
                }
            }
            else
            {
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        MemoryStream ms = new MemoryStream();
                        byte[] binary = reader.ReadBytes((int)fs.Length);
                        ms.Write(binary, 0, binary.Length);

                        return ms;
                    }
                }
            }
        }

        /// <summary>
        /// 判断指定文件是否已加密
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>已加密返回true，否则返回false</returns>
        private bool IsEncrypted(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open))
            {
                byte[] _header = new byte[header.Length];
                fs.Read(_header, 0, header.Length);
                fs.Close();
                fs.Dispose();
                return ArrayEquals(header, _header);
            }
        }

        /// <summary>
        /// 两个数组比较是否相等
        /// </summary>
        /// <param name="a">字节数组1</param>
        /// <param name="b">字节数组2</param>
        /// <returns>相等返回true，否则返回false</returns>
        private bool ArrayEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null)
            {
                return false;
            }
            if (a.Length != b.Length)
            {
                return false;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 将字节数组中每个字节与0xFF做异或运算，返回运算结果
        /// </summary>
        /// <param name="a">字节数组</param>
        /// <returns>字节数组</returns>
        private byte[] XorFF(byte[] a)
        {
            if (a == null)
            {
                return null;
            }
            byte[] result = new byte[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = (byte)(a[i] ^ 0xFF);
            }
            return result;
        }

        /// <summary>
        /// 合并2个数组
        /// </summary>
        /// <param name="a">字节数组1</param>
        /// <param name="b">字节数组2</param>
        /// <returns>合并后字节数组</returns>
        private byte[] MergeBytes(byte[] a, byte[] b)
        {
            if (a == null || b == null)
            {
                return null;
            }
            byte[] result = new byte[a.Length + b.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i];
            }
            for (int i = 0; i < b.Length; i++)
            {
                result[a.Length + i] = b[i];
            }

            return result;
        }
    }
}
