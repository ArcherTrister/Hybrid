using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace NOPI.Tests
{
    public static partial class Fun
    {

        /// <summary>
        /// 获取枚举值得描述信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();   //获取类型  
            MemberInfo[] memberInfos = type.GetMember(en.ToString());   //获取成员  
            if (memberInfos != null && memberInfos.Length > 0)
            {
                DescriptionAttribute[] attrs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];   //获取描述特性  
                if (attrs != null && attrs.Length > 0)
                {
                    return attrs[0].Description;    //返回当前描述  
                }
            }
            return en.ToString();
        }

        /// <summary>
        /// 获取枚举类型的所有元素描述和值(需要设置枚举值的Description属性)
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns>枚举项列表(Key为描述，Value为值)</returns>
        public static NameValueCollection GetEnumItems(Type type)
        {
            NameValueCollection result = new NameValueCollection();
            if (type.IsEnum)
            {
                Type typeDescription = typeof(DescriptionAttribute);
                System.Reflection.FieldInfo[] fields = type.GetFields();
                string strText = string.Empty;
                string strValue = string.Empty;
                foreach (FieldInfo field in fields)
                {
                    if (field.FieldType.IsEnum)
                    {
                        strValue = ((int)type.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
                        object[] arr = field.GetCustomAttributes(typeDescription, true);
                        if (arr.Length > 0)
                        {
                            DescriptionAttribute aa = (DescriptionAttribute)arr[0];
                            strText = aa.Description;
                            result.Add(strText, strValue);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取安全的bool
        /// </summary>
        /// <param name="value">对象。</param>
        /// <returns>安全的bool对象。</returns>
        public static bool ToBool(this object value)
        {
            bool result = false;
            if (value == null || value == DBNull.Value)
            {
                return result;
            }
            if (!bool.TryParse(value.ToString(), out result))
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 获取安全的bool?
        /// </summary>
        /// <param name="value">对象。</param>
        /// <returns>安全的bool?对象。</returns>
        public static bool? ToNullableBool(this object value)
        {
            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return null;
            }
            bool result = false;
            if (!bool.TryParse(value.ToString(), out result))
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 获取安全的int值
        /// </summary>
        /// <param name="value">任意object</param>
        /// <returns></returns>
        public static int ToInt32(this object value)
        {
            int result = 0;
            if (value == null || value == DBNull.Value)
            {
                return result;
            }
            if (!int.TryParse(value.ToString(), out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 获取安全的int?值
        /// </summary>
        /// <param name="value">任意object</param>
        /// <returns>int?值</returns>
        public static int? ToNullableInt(this object value)
        {
            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return null;
            }
            int result = 0;
            if (!int.TryParse(value.ToString(), out result))
            {
                result = 0;
            }
            return result;
        }/// <summary>
         /// 获取安全的short
         /// </summary>
         /// <param name="value">对象。</param>
         /// <returns>安全的short对象。</returns>
        public static short ToShort(this object value)
        {
            short result = 0;
            if (value == null || value == DBNull.Value)
            {
                return result;
            }
            if (!short.TryParse(value.ToString(), out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 获取安全的short?
        /// </summary>
        /// <param name="value">对象。</param>
        /// <returns>安全的short?对象。</returns>
        public static short? ToNullableShort(this object value)
        {
            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return null;
            }
            short result = 0;
            if (!short.TryParse(value.ToString(), out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 获取安全的long
        /// </summary>
        /// <param name="value">对象。</param>
        /// <returns>安全的short对象。</returns>
        public static long ToLong(this object value)
        {
            long result = 0;
            if (value == null || value == DBNull.Value)
            {
                return result;
            }
            if (!long.TryParse(value.ToString(), out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 获取安全的long?
        /// </summary>
        /// <param name="value">对象。</param>
        /// <returns>安全的long?对象。</returns>
        public static long? ToNullableLong(this object value)
        {
            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return null;
            }
            long result = 0;
            if (!long.TryParse(value.ToString(), out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 获取安全的decimal值
        /// </summary>
        /// <param name="value">任意object</param>
        /// <returns>decimal值</returns>
        public static decimal ToDecimal(this object value)
        {
            decimal result = 0;
            if (value == null || value == DBNull.Value)
            {
                return result;
            }
            if (!decimal.TryParse(value.ToString(), out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 获取安全的decimal?
        /// </summary>
        /// <param name="value">对象。</param>
        /// <returns>安全的decimal?对象。</returns>
        public static decimal? ToNullableDecimal(this object value)
        {
            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return null;
            }
            decimal result = 0;
            if (!decimal.TryParse(value.ToString(), out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 四舍五入并保留指定小数位
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="decimals">小数位</param>
        /// <returns>数值</returns>
        public static decimal RoundEx(this decimal value, int decimals = 2)
        {
            return Math.Round(value, decimals);
        }

        /// <summary>
        /// 获取安全的float
        /// </summary>
        /// <param name="value">对象。</param>
        /// <returns>安全的float对象。</returns>
        public static float ToFloat(this object value)
        {
            float result = 0f;
            if (value == null || value == DBNull.Value)
            {
                return result;
            }
            if (!float.TryParse(value.ToString(), out result))
            {
                result = 0f;
            }
            return result;
        }

        /// <summary>
        /// 获取安全的float?
        /// </summary>
        /// <param name="value">对象。</param>
        /// <returns>安全的float?对象。</returns>
        public static float? ToNullableFloat(this object value)
        {
            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return null;
            }
            float result = 0;
            if (!float.TryParse(value.ToString(), out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 获取安全的double
        /// </summary>
        /// <param name="value">对象。</param>
        /// <returns>安全的double对象。</returns>
        public static double ToDouble(this object value)
        {
            double result = 0f;
            if (value == null || value == DBNull.Value)
            {
                return result;
            }
            if (!double.TryParse(value.ToString(), out result))
            {
                result = 0f;
            }
            return result;
        }

        /// <summary>
        /// 获取安全的double?
        /// </summary>
        /// <param name="value">对象。</param>
        /// <returns>安全的double对象。</returns>
        public static double? ToNullableDouble(this object value)
        {
            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return null;
            }
            double result = 0;
            if (!double.TryParse(value.ToString(), out result))
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 四舍五入并保留指定小数位
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="decimals">小数位</param>
        /// <returns>数值</returns>
        public static double Round(this double value, int decimals = 2)
        {
            return Math.Round(value, decimals);
        }

        /// <summary>
        /// 获取安全的字符串
        /// </summary>
        /// <param name="value">对象</param>
        /// <returns>安全的字符串对象</returns>
        public static string ToSafeString(this object value)
        {
            if (value == DBNull.Value || value == null)
            {
                return string.Empty;
            }
            return value.ToString();
        }

        /// <summary>
        /// 获取安全的Trim后的字符串
        /// </summary>
        /// <param name="value">对象</param>
        /// <returns>安全的字符串对象</returns>
        public static string ToTrimedString(this object c)
        {
            return c.ToSafeString().Trim();
        }

        /// <summary>
        /// 获取安全的DateTime
        /// </summary>
        /// <param name="value">对象。</param>
        /// <returns>安全的DateTime对象。</returns>
        public static DateTime ToDateTime(this object value)
        {
            DateTime result = DateTime.MinValue;
            if (value == null || value == DBNull.Value)
            {
                return result;
            }
            if (!DateTime.TryParse(value.ToString(), out result))
            {
                result = DateTime.MinValue;
            }
            return result;
        }

        /// <summary>
        /// 获取安全的DateTime?
        /// </summary>
        /// <param name="value">对象。</param>
        /// <returns>安全的DateTime?对象。</returns>
        public static DateTime? ToNullableDateTime(this object value)
        {
            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return null;
            }
            DateTime result = DateTime.MinValue;
            if (!DateTime.TryParse(value.ToString(), out result))
            {
                result = DateTime.MinValue;
            }
            return result;
        }

        /// <summary>
        /// 为不包含时分秒的日期对象添加当前时间时分秒
        /// </summary>
        /// <param name="date">日期对象</param>
        /// <returns>时间对象</returns>
        public static DateTime AppendTime(this DateTime date)
        {
            DateTime now = DateTime.Now;
            return new DateTime(date.Year, date.Month, date.Day, now.Hour, now.Minute, now.Second);
        }

        public static string NumberToChinese(this int number)
        {
            string res = string.Empty;
            if (number == 0)
            {
                return "零";
            }
            string str = number.ToString();
            for (int i = 0; i < str.Length; i++)
            {
                var substr = str.Substring(i, str.Length - i);
                string schar = substr.Substring(0, 1);
                if (substr.Length > 1)
                {
                    switch (schar)
                    {
                        case "1":
                            if (!string.IsNullOrEmpty(res) || str.Length > 2)
                            {
                                res += "一";
                            }
                            break;
                        case "2":
                            res += "二";
                            break;
                        case "3":
                            res += "三";
                            break;
                        case "4":
                            res += "四";
                            break;
                        case "5":
                            res += "五";
                            break;
                        case "6":
                            res += "六";
                            break;
                        case "7":
                            res += "七";
                            break;
                        case "8":
                            res += "八";
                            break;
                        case "9":
                            res += "九";
                            break;
                    }
                    switch (substr.Length)
                    {
                        case 2:
                        case 6:
                            res += "十";
                            break;
                        case 3:
                        case 7:
                            res += "百";
                            break;
                        case 4:
                            res += "千";
                            break;
                        case 5:
                            res += "万";
                            break;
                        default:
                            res += "";
                            break;
                    }
                }
                else
                {
                    switch (schar)
                    {
                        case "1":
                            res += "一";
                            break;
                        case "2":
                            res += "二";
                            break;
                        case "3":
                            res += "三";
                            break;
                        case "4":
                            res += "四";
                            break;
                        case "5":
                            res += "五";
                            break;
                        case "6":
                            res += "六";
                            break;
                        case "7":
                            res += "七";
                            break;
                        case "8":
                            res += "八";
                            break;
                        case "9":
                            res += "九";
                            break;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 将html文本过滤为纯文本
        /// </summary>
        /// <param name="Htmlstring">需要过滤的文本</param>
        /// <returns>过滤后的文本</returns>
        public static string FilterHTML(string Htmlstring)  //替换HTML标记   
        {
            if (string.IsNullOrWhiteSpace(Htmlstring))
            {
                return string.Empty;
            }
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>[\s\S]*?</script>", "", RegexOptions.IgnoreCase);
            //删除样式
            Htmlstring = Regex.Replace(Htmlstring, @"<style[^>]*?>[\s\S]*?</style>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "\"", "");
            Htmlstring = Regex.Replace(Htmlstring, "'", "");
            Htmlstring = Regex.Replace(Htmlstring, "alert%28", "", RegexOptions.IgnoreCase);

            return Htmlstring;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string stringIntercept(string strSource, int len)
        {
            if (string.IsNullOrWhiteSpace(strSource))
            {
                return string.Empty;
            }
            return SubString(strSource, len);
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="inputString">原始字符串</param>
        /// <param name="length">截取长度</param>
        /// <returns>截取后的字符串</returns>
        public static string SubString(string inputString, int length)
        {
            if (Encoding.UTF8.GetByteCount(inputString) <= length * 2)
            {
                return inputString;
            }
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
                tempString += inputString.Substring(i, 1);
                if (tempLen >= (length - 1) * 2)
                    break;
            }
            //如果截过则加上半个省略号
            if (System.Text.Encoding.Default.GetBytes(inputString).Length > length)
                tempString += "..";
            return tempString;
        }

        static Regex rg = new Regex("(\\d{3})\\d{5}(\\d{3})");
        public static string ReplacePhoneNO(string phoneNO)
        {
            if (string.IsNullOrEmpty(phoneNO)) return string.Empty;
            if (phoneNO.Length < 11) return phoneNO;
            var result = rg.Replace(phoneNO, "$1****$2");
            return result;
        }

        /// <summary>
        /// 将传入的字符串中间部分字符替换成特殊字符
        /// </summary>
        /// <param name="value">需要替换的字符串</param>
        /// <param name="startLen">前保留长度</param>
        /// <param name="endLen">尾保留长度</param>
        /// <param name="replaceChar">特殊字符</param>
        /// <returns>被特殊字符替换的字符串</returns>
        public static string ReplaceWithSpecialChar(string value, int startLen = 4, int endLen = 4, char specialChar = '*')
        {
            try
            {
                int lenth = value.Length - startLen - endLen;
                string replaceStr = value.Substring(startLen, lenth);
                string specialStr = string.Empty;
                for (int i = 0; i < replaceStr.Length; i++)
                {
                    specialStr += specialChar;
                }
                value = value.Replace(replaceStr, specialStr);
            }
            catch (Exception)
            {
                return value;
            }
            return value;
        }

        public static bool IsPhone(string phone)
        {
            Regex phoneReg = new Regex("^(0|86|17951)?(13[0-9]|15[012356789]|17[03678]|18[0-9]|14[57])[0-9]{8}$", RegexOptions.Compiled);
            return phoneReg.IsMatch(phone);
        }

        /// <summary>  
        /// 验证身份证合理性  
        /// </summary>  
        /// <param name="Id"></param>  
        /// <returns></returns>  
        public static bool CheckIDCard(string idNumber)
        {
            if (idNumber.Length == 18)
            {
                bool check = CheckIDCard18(idNumber);
                return check;
            }
            else if (idNumber.Length == 15)
            {
                bool check = CheckIDCard15(idNumber);
                return check;
            }
            else
            {
                return false;
            }
        }

        /// <summary>  
        /// 18位身份证号码验证  
        /// </summary>  
        private static bool CheckIDCard18(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber.Remove(17), out n) == false
                || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = idNumber.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
            {
                return false;//校验码验证  
            }
            return true;//符合GB11643-1999标准  
        }


        /// <summary>  
        /// 16位身份证号码验证  
        /// </summary>  
        private static bool CheckIDCard15(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            return true;
        }

    }
}
