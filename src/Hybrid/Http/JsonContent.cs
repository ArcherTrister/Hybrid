using Newtonsoft.Json;

using System.Net.Http;
using System.Text;

namespace Hybrid.Http
{
    /// <summary>
    /// Json的HttpContent
    /// </summary>
    public class JsonContent : StringContent
    {
        /// <summary>
        /// 初始化一个<see cref="JsonContent"/>类型的新实例
        /// </summary>
        public JsonContent(object obj)
            : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }
    }
}