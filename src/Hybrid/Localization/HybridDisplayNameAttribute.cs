using System.ComponentModel;

namespace Hybrid.Localization
{
    public class HybridDisplayNameAttribute : DisplayNameAttribute
    {
        public override string DisplayName => LocalizationHelper.GetString(SourceName, Key);

        public string SourceName { get; set; }
        public string Key { get; set; }

        public HybridDisplayNameAttribute(string sourceName, string key)
        {
            SourceName = sourceName;
            Key = key;
        }
    }
}