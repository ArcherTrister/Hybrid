// -----------------------------------------------------------------------
//  <copyright file="JsonContent.cs" company="Hybrid开源团队">
//      Copyright (c) 2014-2019 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2019-11-08 16:45</last-date>
// -----------------------------------------------------------------------

using System.Net.Http;
using System.Text;

using Newtonsoft.Json;


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