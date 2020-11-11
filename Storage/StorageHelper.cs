using System;
using System.IO;

namespace Kaiheila.Cqhttp.Storage
{
    /// <summary>
    /// 提供访问应用存储能力的帮助类型。
    /// </summary>
    public static class StorageHelper
    {
        /// <summary>
        /// 从存储根目录中获取文件。
        /// </summary>
        /// <param name="filename">要获取的文件的文件名。</param>
        /// <returns>文件的完整路径。</returns>
        public static string GetRootFilePath(string filename) => Path.Combine(GetRootPath(), filename);

        /// <summary>
        /// 从存储根目录中获取分类文件夹。
        /// </summary>
        /// <param name="sectionName">分类的名称。</param>
        /// <returns>文件夹的完整路径。</returns>
        public static string GetSectionFolderPath(string sectionName)
        {
            string folderPath = Path.Combine(GetRootPath(), @$"{sectionName}\");
            Directory.CreateDirectory(folderPath);
            return folderPath;
        }

        /// <summary>
        /// 从分类存储目录中获取文件。
        /// </summary>
        /// <param name="sectionName">分类的名称。</param>
        /// <param name="filename">要获取的文件的文件名。</param>
        /// <returns>文件的完整路径。</returns>
        public static string GetSectionFilePath(string sectionName, string filename) =>
            Path.Combine(GetSectionFolderPath(sectionName), filename);

        /// <summary>
        /// 从存储目录中获取图片文件夹。
        /// </summary>
        /// <returns>文件夹的完整路径。</returns>
        public static string GetImagesFolderPath() =>
            GetSectionFolderPath("images");

        /// <summary>
        /// 从存储目录中获取临时文件夹。
        /// </summary>
        /// <returns>文件夹的完整路径。</returns>
        public static string GetTempFolderPath() =>
            GetSectionFolderPath("temp");

        /// <summary>
        /// 从存储目录中获取临时文件。
        /// </summary>
        /// <param name="file">要获取的文件的文件名。</param>
        /// <returns>文件的完整路径。</returns>
        public static string GetTempFilePath(string file) =>
            Path.Combine(GetTempFolderPath(), file);

        /// <summary>
        /// 获取存储根目录的路径。
        /// </summary>
        /// <returns>存储根目录的路径。</returns>
        private static string GetRootPath()
        {
            string rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"storage\");
            Directory.CreateDirectory(rootPath);
            return rootPath;
        }
    }
}
