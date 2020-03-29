using System;
using System.IO;
using System.Reflection;

namespace Hybrid.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 嵌入式资源转字符串
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="resourceName">资源名称【全称】</param>
        /// <returns></returns>
        public static string GetStringByResource(this Assembly assembly, string resourceName)
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException(
                        $"Requested resource `{resourceName}` was not found in the assembly `{assembly}`.");
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
