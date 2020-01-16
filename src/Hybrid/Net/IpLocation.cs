// -----------------------------------------------------------------------
//  <copyright file="IpLocation.cs" company="com.esoftor">
//      Copyright ? 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2014-10-14 1:26</last-date>
// -----------------------------------------------------------------------

namespace Hybrid.Net
{
    /// <summary>
    /// IP位置信息类
    /// </summary>
    public class IpLocation
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// IP地址所属国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 位置信息
        /// </summary>
        public string Local { get; set; }
    }
}