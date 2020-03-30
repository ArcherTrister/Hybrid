using Microsoft.AspNetCore.Mvc.ModelBinding;

using System.Linq;
using System.Text;

namespace Hybrid.AspNetCore.Extensions
{
    public static class ModelStateExtensions
    {
        /// <summary>
        /// 获取验证消息提示并格式化提示
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string GetValidationSummary(this ModelStateDictionary modelStateDictionary, string separator = "\r\n")
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in modelStateDictionary)
            {
                var state = item.Value;
                var message = state.Errors.FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.ErrorMessage))?.ErrorMessage;
                if (string.IsNullOrWhiteSpace(message))
                {
                    message = state.Errors.FirstOrDefault(o => o.Exception != null)?.Exception.Message;
                }
                if (string.IsNullOrWhiteSpace(message)) continue;
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append(separator);
                }
                stringBuilder.Append(message);
            }
            return stringBuilder.ToString();
        }
    }
}