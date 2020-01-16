
using Hybrid.Extensions;

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hybrid.Localization.Dictionaries
{
    internal static class Utf8Helper
    {
        public static string ReadStringFromStream(Stream stream)
        {
            byte[] bytes = stream.GetAllBytes();
            int skipCount = HasBom(bytes) ? 3 : 0;
            return Encoding.UTF8.GetString(bytes, skipCount, bytes.Length - skipCount);
        }

        private static bool HasBom(IReadOnlyList<byte> bytes)
        {
            if (bytes.Count < 3)
            {
                return false;
            }

            return bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF;
        }
    }
}