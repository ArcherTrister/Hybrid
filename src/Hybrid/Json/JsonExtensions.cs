// -----------------------------------------------------------------------
//  <copyright file="JsonExtensions.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2018 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-06-25 17:29</last-date>
// -----------------------------------------------------------------------

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace Hybrid.Json
{
    /// <summary>
    /// Json辅助扩展操作
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// 将对象转换为JSON字符串
        /// </summary> 
        /// <param name="obj">要转换的对象</param>
        /// <param name="camelCase">是否小写名称</param>
        /// <param name="indented"></param>
        /// <returns></returns>
        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            if (camelCase)
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
            if (indented)
            {
                settings.Formatting = Formatting.Indented;
            }
            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}