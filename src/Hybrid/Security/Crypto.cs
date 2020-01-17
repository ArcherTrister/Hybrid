using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Hybrid.Security
{
    /// <summary>
    /// 加密解密
    /// </summary>
    public static class Crypto
    {
        //注：Des加密，改变 des.Mode = CipherMode.ECB; des.Padding = PaddingMode.PKCS7; 得到的值不同；
        //    跟java项目对接，通过改变这两个参数达到与java一致。

        /// <summary>
        /// C# DES加密方法
        /// </summary>
        /// <param name="decryptedValue">要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns>加密后的字符串</returns>
        public static string DesEncrypt(string decryptedValue, string key, string iv)
        {
            using (var sa
                = new DESCryptoServiceProvider { Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(iv) })
            {
                using (ICryptoTransform ct = sa.CreateEncryptor())
                {
                    byte[] by = Encoding.UTF8.GetBytes(decryptedValue);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct,
                            CryptoStreamMode.Write))
                        {
                            cs.Write(by, 0, by.Length);
                            cs.FlushFinalBlock();
                        }

                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// C# DES加密方法
        /// </summary>
        /// <param name="decryptedValue">要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string DesEncrypt(string decryptedValue, string key = "Gy4aFj7z")
        {
            using (var des = new DESCryptoServiceProvider())
            {
                //建立加密对象的密钥和偏移量
                //原文使用ASCIIEncoding.ASCII方法的GetBytes方法
                //使得输入密码必须输入英文文本
                byte[] keyBytes = Encoding.ASCII.GetBytes(key);
                des.Key = keyBytes;
                des.IV = keyBytes;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(decryptedValue);
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        var ret = new StringBuilder();
                        foreach (byte b in ms.ToArray())
                        {
                            ret.AppendFormat("{0:X2}", b);
                        }

                        return ret.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// C# DES解密方法
        /// </summary>
        /// <param name="encryptedValue">待解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns>解密后的字符串</returns>
        public static string DesDecrypt(string encryptedValue, string key, string iv)
        {
            using (var sa =
                new DESCryptoServiceProvider
                { Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(iv) })
            {
                using (ICryptoTransform ct = sa.CreateDecryptor())
                {
                    byte[] byt = Convert.FromBase64String(encryptedValue);

                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                        {
                            cs.Write(byt, 0, byt.Length);
                            cs.FlushFinalBlock();
                        }

                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// C# DES解密方法
        /// </summary>
        /// <param name="encryptedValue">待解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string DesDecrypt(string encryptedValue, string key = "Gy4aFj7z")
        {
            using (var des = new DESCryptoServiceProvider())
            {
                byte[] keyBytes = Encoding.ASCII.GetBytes(key);
                des.Key = keyBytes;
                des.IV = keyBytes;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        var inputByteArray = new byte[encryptedValue.Length / 2];
                        for (var x = 0; x < encryptedValue.Length / 2; x++)
                        {
                            int i = Convert.ToInt32(encryptedValue.Substring(x * 2, 2), 16);
                            inputByteArray[x] = (byte)i;
                        }

                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();

                        return Encoding.Default.GetString(ms.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// 16位MD5加密转小写
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string Md5Hex(byte[] buffer)
        {
            var md5 = new MD5CryptoServiceProvider();
            string mySign = BitConverter.ToString(md5.ComputeHash(buffer));
            mySign = mySign.Replace("-", "");
            return mySign.ToLower();
        }

        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Md5Encrypt16(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Md5Encrypt32(string password)
        {
            string cl = password;
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            return s.Aggregate("", (current, t) => current + t.ToString("X"));
        }

        /// <summary>
        /// Base64MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Md5Encrypt64(string password)
        {
            string cl = password;
            //string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            return Convert.ToBase64String(s);
        }
    }
}