using Newtonsoft.Json.Serialization;

using System.Text;

namespace Hybrid.Json
{
    /// <summary>
    /// 下划线(+大写)转驼峰
    /// </summary>
    public class CamelCasePropertyNamesContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            return UnderlineSplitToCamelCase(propertyName);
        }

        private string UnderlineSplitToCamelCase(string name)
        {
            StringBuilder result = new StringBuilder();
            // 快速检查
            if (name == null || string.IsNullOrEmpty(name))
            {
                // 没必要转换
                return "";
            }
            else if (!name.Contains("_"))
            {
                // 不含下划线，仅将首字母小写
                return name.Substring(0, 1).ToLower() + name.Substring(1);
            }
            // 用下划线将原始字符串分割
            string[] camels = name.Split("_");
            foreach (string camel in camels)
            {
                // 跳过原始字符串中开头、结尾的下换线或双重下划线
                if (string.IsNullOrEmpty(camel))
                {
                    continue;
                }
                // 处理真正的驼峰片段
                if (result.Length == 0)
                {
                    // 第一个驼峰片段，全部字母都小写
                    result.Append(camel.ToLower());
                }
                else
                {
                    // 其他的驼峰片段，首字母大写
                    result.Append(camel.Substring(0, 1).ToUpper());
                    result.Append(camel.Substring(1).ToLower());
                }
            }
            return result.ToString();
        }
    }
}
