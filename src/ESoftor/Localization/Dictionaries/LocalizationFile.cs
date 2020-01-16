using System.Reflection;

namespace Hybrid.Localization.Dictionaries
{
    /// <summary>
    /// 本地化文件参数
    /// </summary>
    public sealed class LocalizationFile
    {
        /// <summary>
        /// Assembly that contains embedded xml/json files
        /// </summary>
        public Assembly Assembly { get; set; }

        /// <summary>
        /// Namespace of the embedded xml/json dictionary files
        /// </summary>
        public string RootNamespace { get; set; }

        /// <summary>
        /// Path of the dictionary that contains all related XML/JSON files
        /// </summary>
        public string DirectoryPath { get; set; }

        /// <summary>
        /// 本地化文件类型
        /// </summary>
        public LocalizationFileType LocalizationFileType { get; set; }
    }
}