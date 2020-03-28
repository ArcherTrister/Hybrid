using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Hybrid.Data
{
    /// <summary>
    /// ASCII值排序
    /// </summary>
    public class OrdinalComparer : IComparer<string>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(string x, string y)
        {
            return string.CompareOrdinal(x, y);
        }

        ////使用
        ////SortedDictionary<string, string> sDic 待排序的键值对
        //var sArr = sDic.OrderBy(x => x.Key, new OrdinalComparer()).ToDictionary(x => x.Key, y => y.Value);
        ////然后 foreach sArr 就OK了

        /// <summary>
        /// ASCII值排序参数
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public static string FormatParam(SortedDictionary<string, string> keyValuePairs)
        {
            var arrs = keyValuePairs.OrderBy(x => x.Key, new OrdinalComparer()).ToDictionary(x => x.Key, y => y.Value);
            StringBuilder sb = new StringBuilder();
            foreach (var arr in arrs)
            {
                string key = arr.Key;
                string val = arr.Value;
                if (!string.IsNullOrWhiteSpace(key))
                {
                    sb.Append(key + "=" + val + "&");
                }
            }
            var str = sb.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                return str.Substring(0, str.Length - 1);
            }
            return "";
        }

        /// <summary>
        /// ASCII值排序参数
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <param name="urlEncode"></param>
        /// <param name="keyToLower"></param>
        /// <returns></returns>
        public static string FormatParam(SortedDictionary<string, string> keyValuePairs, bool urlEncode, bool keyToLower)
        {
            var arrs = keyValuePairs.OrderBy(x => x.Key, new OrdinalComparer()).ToDictionary(x => x.Key, y => y.Value);
            StringBuilder sb = new StringBuilder();
            foreach (var arr in arrs)
            {
                string key = arr.Key;
                string val = arr.Value;
                if (!string.IsNullOrWhiteSpace(key))
                {
                    if (urlEncode)
                    {
                        val = HttpUtility.UrlEncode(val);
                    }
                    if (keyToLower)
                    {
                        sb.Append(key.ToLower() + "=" + val);
                    }
                    else
                    {
                        sb.Append(key + "=" + val);
                    }
                    sb.Append("&");
                }
            }
            var str = sb.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        //**
        // *
        //* @Title: formatParamMap
        //* @Description: 对所有传入参数按照字段名的 ASCII 码从小到大排序（字典序），生成需要MD5加密的url参数串
        //* @param @param paraMap
        //* @param @param urlEncode
        //* @param @param keyToLower
        //* @param @return    设定文件
        //* @return String    返回类型
        //* @date 2017年12月7日 上午11:28:03
        //* @throws
        // */
        //public static String formatParamMap(Map<String, String> paraMap, boolean urlEncode,
        //        boolean keyToLower)
        //{
        //    String buff = "";
        //    Map<String, String> tmpMap = paraMap;
        //    try
        //    {
        //        List<Map.Entry<String, String>> infoIds =
        //                new ArrayList<Map.Entry<String, String>>(tmpMap.entrySet());
        //        // 对所有传入参数按照字段名的 ASCII 码从小到大排序（字典序）
        //        Collections.sort(infoIds, new Comparator<Map.Entry<String, String>>() {
        //            @Override
        //            public int compare(Map.Entry<String, String> o1, Map.Entry<String, String> o2)
        //        {
        //            return (o1.getKey()).toString().compareTo(o2.getKey());
        //        }
        //    });
        //    // 构造URL 键值对的格式
        //    StringBuilder buf = new StringBuilder();
        //    for (Map.Entry<String, String> item : infoIds)
        //    {
        //        if (StringUtils.isNotBlank(item.getKey()))
        //        {
        //            String key = item.getKey();
        //            String val = item.getValue();
        //            if (urlEncode)
        //            {
        //                val = URLEncoder.encode(val, Constants.CHARSET);
        //            }
        //            if (keyToLower)
        //            {
        //                buf.append(key.toLowerCase() + "=" + val);
        //            }
        //            else
        //            {
        //                buf.append(key + "=" + val);
        //            }
        //            buf.append("&");
        //        }

        //    }
        //    buff = buf.toString();
        //    if (buff.isEmpty() == false)
        //    {
        //        buff = buff.substring(0, buff.length() - 1);
        //    }
        //} catch (Exception e) {
        //        return null;
        //    }
        //    return buff;
        //}

        //class Program
        //{
        //    static void Main(string[] args)
        //    {
        //        Dictionary<string, string> dic = new Dictionary<string, string>();
        //        dic.Add("aname", "bjson");
        //        dic.Add("url", "http://www.bejson.com");
        //        dic.Add("address", "科技园路");
        //        dic.Add("country", "中国");

        //        #region 升序方法1
        //        //升序方法1：
        //        Dictionary<string, string> ascdic = dic.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value.ToString());//对key进行升序
        //                                                                                                                      //遍历元素
        //        foreach (KeyValuePair<string, string> kv in ascdic)
        //        {
        //            Console.WriteLine("方法一升序以后的key列表为：" + kv.Key + ", value为：" + kv.Value);
        //        }
        //        #endregion

        //        #region 升序方法2：直接使用SortedDictionary
        //        //升序方法2：直接使用SortedDictionary
        //        SortedDictionary<string, string> sorDic = new SortedDictionary<string, string>();
        //        sorDic.Add("street", "学院路");
        //        sorDic.Add("city", "上海");
        //        sorDic.Add("cab", "30");
        //        sorDic.Add("url", "http://www.google.com");
        //        //遍历元素
        //        foreach (KeyValuePair<string, string> kv in sorDic)
        //        {
        //            Console.WriteLine("方法2的升序以后顺序为key：" + kv.Key + ",  value为：" + kv.Value);
        //        }
        //        #endregion

        //        #region 升序方法3
        //        //升序排序方法2的简单版本
        //        SortedDictionary<string, string> sac = new SortedDictionary<string, string>(dic);
        //        foreach (KeyValuePair<string, string> kv in sorDic)
        //        {
        //            Console.WriteLine("方法2的另一个版本以后顺序为key：" + kv.Key + ",  value为：" + kv.Value);
        //        }
        //        #endregion

        //        #region 升序方法4
        //        //升序方法3
        //        ArrayList arrayList = new ArrayList();
        //        arrayList.AddRange(dic.Keys);
        //        arrayList.Sort();

        //        //遍历元素
        //        StringBuilder sbu = new StringBuilder();
        //        Dictionary<string, string> ascDic = new Dictionary<string, string>();
        //        foreach (string al in arrayList)//注意arrayList与al的数据类型是子集关系
        //        {
        //            sbu.Append(al).Append(dic[al]);//得到数据为key1value1key2value2
        //            ascDic.Add(al, dic[al]);

        //        }
        //        foreach (KeyValuePair<string, string> kv in ascDic)
        //        {
        //            Console.WriteLine("方法3的数据封装以后顺序为key：" + kv.Key + ",  value为：" + kv.Value);
        //        }
        //        Console.WriteLine("方法3升序以后的顺序为：" + sbu);

        //        #endregion

        //        //降序排列
        //        Console.WriteLine("---------------------降序排列结果如下：---------------------");

        //        #region 降序方法1
        //        //降序方法一：
        //        Dictionary<string, string> desDic = dic.OrderByDescending(o => o.Key).ToDictionary(o => o.Key, p => p.Value.ToString());
        //        foreach (KeyValuePair<string, string> kv in desDic)
        //        {
        //            Console.WriteLine("第一种降序方式运行结果key为：" + kv.Key + ", value为" + kv.Value);
        //        }
        //        #endregion

        //        #region 降序方法三
        //        //降序方法二：
        //        Dictionary<string, string> des2Dic = new Dictionary<string, string>();
        //        foreach (KeyValuePair<string, string> kv in sorDic.Reverse())//Reverse()方法为反转元素顺序
        //        {
        //            des2Dic.Add(kv.Key, kv.Value);
        //            Console.WriteLine("将SortedDictionary<string, string>升序以后的数据进行降序的key为：" + kv.Key + ", value为：" + kv.Value);
        //        }
        //        #endregion

        //        #region 降序方法三
        //        //降序方法三：
        //        arrayList.Reverse();
        //        StringBuilder sub = new StringBuilder();
        //        Dictionary<string, string> desrDic = new Dictionary<string, string>();
        //        foreach (string al in arrayList)//注意arrayList与al的数据类型是子集关系
        //        {
        //            sub.Append(al).Append(dic[al]);//得到数据为key1value1key2value2
        //            desrDic.Add(al, dic[al]);

        //        }
        //        foreach (KeyValuePair<string, string> kv in desrDic)
        //        {
        //            Console.WriteLine("方法3的ArrayList逆序以后顺序为key：" + kv.Key + ",  value为：" + kv.Value);
        //        }
        //        Console.WriteLine("方法3降序以后的顺序为：" + sub);

        //        #endregion

        //    }
        //}
    }
}