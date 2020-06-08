/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   ZipHelper.cs
* 功能描述： 压缩帮助类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-24 15:27:51
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-24 15:27:51		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using TechScan.Tool.U8.ServiceDeploy.Base.Exceptions;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Utils
{
    /// <summary>
    /// 压缩帮助类
    /// </summary>
    public class ZipHelper
    {
        private static readonly char s_pathSeperator = '/';

        public static List<FileSystemInfo> GetSelectDeployFiles(List<string> fileList)
        {
            List<FileSystemInfo> findlist = new List<FileSystemInfo>();
            foreach (var filePath in fileList)
            {
                findlist.Add(new FileInfo(filePath));
            }
            return findlist;
        }

        public static List<FileSystemInfo> GetFullFileInfo(List<string> fileList, string folderPath)
        {
            List<FileSystemInfo> findlist = new List<FileSystemInfo>();
            List<FileSystemInfo> folderlist = new List<FileSystemInfo>();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var filePath in fileList)
            {
                var fullPath = Path.Combine(folderPath, filePath);
                var fileArr = filePath.Split('/');
                if (fileArr.Length == 1)
                {
                    if (dic.ContainsKey(fullPath))
                    {
                        continue;
                    }

                    dic.Add(fullPath, filePath);
                    findlist.Add(new FileInfo(fullPath));
                }
                else
                {
                    for (int i = 0; i < fileArr.Length; i++)
                    {
                        if (i == fileArr.Length - 1)
                        {
                            if (dic.ContainsKey(fullPath))
                            {
                                continue;
                            }

                            dic.Add(fullPath, filePath);
                            findlist.Add(new FileInfo(Path.Combine(folderPath, string.Join(Path.DirectorySeparatorChar.ToString(), fileArr))));
                        }
                        else
                        {
                            string foldPath = Path.Combine(folderPath, string.Join(Path.DirectorySeparatorChar.ToString(), fileArr.Take(i + 1).ToList()));
                            if (Directory.Exists(foldPath))
                            {
                                if (dic.ContainsKey(foldPath))
                                {
                                    continue;
                                }

                                dic.Add(foldPath, foldPath);
                                folderlist.Add(new DirectoryInfo(foldPath));
                            }
                        }
                    }
                }

            }

            folderlist.AddRange(findlist);

            return folderlist;
        }

        public static FileSystemInfo[] FindFileDir(string beginpath)
        {
            List<FileSystemInfo> findlist = new List<FileSystemInfo>();

            /* I begin a recursion, following the order:
             * - Insert all the files in the current directory with the recursion
             * - Insert all subdirectories in the list and rebegin the recursion from there until the end
             */
            RecurseFind(beginpath, findlist);

            return findlist.ToArray();
        }

        private static void RecurseFind(string path, List<FileSystemInfo> list)
        {
            string[] fl = Directory.GetFiles(path);
            string[] dl = Directory.GetDirectories(path);
            if (fl.Length > 0 || dl.Length > 0)
            {
                //I begin with the files, and store all of them in the list
                foreach (string s in fl)
                    list.Add(new FileInfo(s));
                //I then add the directory and recurse that directory, the process will repeat until there are no more files and directories to recurse
                foreach (string s in dl)
                {
                    if (s.EndsWith("\\.git")) continue;
                    list.Add(new DirectoryInfo(s));
                    RecurseFind(s, list);
                }
            }
        }

        public static byte[] DoCreateFromDirectory(string sourceDirectoryName, List<string> fileList, CompressionLevel? compressionLevel, bool includeBaseDirectory, List<string> ignoreList = null, Func<int, bool> progress = null, bool isSelectDeploy = false/*, Logger logger = null*/)
        {
            //if (ignoreList != null)
            //{
            //    ignoreList.Add("/.git?");
            //}
            var haveFile = false;
            sourceDirectoryName = Path.GetFullPath(sourceDirectoryName);
            using (var outStream = new MemoryStream())
            {
                using (ZipArchive destination = new ZipArchive(outStream, ZipArchiveMode.Create, false))
                {
                    bool flag = true;
                    DirectoryInfo directoryInfo = new DirectoryInfo(sourceDirectoryName);
                    string fullName = directoryInfo.FullName;
                    if (includeBaseDirectory && directoryInfo.Parent != null)
                        fullName = directoryInfo.Parent.FullName;
                    var allFile = isSelectDeploy ? GetSelectDeployFiles(fileList) : GetFullFileInfo(fileList, sourceDirectoryName);
                    var allFileLength = allFile.Count();
                    var index = 0;

                    foreach (FileSystemInfo enumerateFileSystemInfo in allFile)
                    {
                        index++;
                        var lastProgressNumber = (((long)index * 100 / allFileLength));
                        if (progress != null)
                        {
                            var r = progress.Invoke((int)lastProgressNumber);
                            if (r)
                            {
                                throw new Exception("deploy task was canceled!");
                            }
                        }

                        flag = false;
                        int length = enumerateFileSystemInfo.FullName.Length - fullName.Length;
                        string entryName = EntryFromPath(enumerateFileSystemInfo.FullName, fullName.Length, length);
                        var mathchEntryName = includeBaseDirectory ? entryName.Substring(directoryInfo.Name.Length) : "/" + entryName;
                        if (ignoreList != null && ignoreList.Count > 0)
                        {
                            var haveMatch = false;
                            foreach (var ignorRule in ignoreList)
                            {
                                try
                                {
                                    if (ignorRule.StartsWith("*"))
                                    {
                                        var ignorRule2 = ignorRule.Substring(1);
                                        if (mathchEntryName.EndsWith(ignorRule2))
                                        {
                                            haveMatch = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        var isMatch = Regex.Match(mathchEntryName, ignorRule, RegexOptions.IgnoreCase);//忽略大小写
                                        if (isMatch.Success)
                                        {
                                            haveMatch = true;
                                            break;
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    throw new Exception($"Ignore Rule 【{ignorRule}】 regular error:" + ex.Message);
                                }
                            }

                            if (haveMatch)
                            {
                                continue;
                            }
                        }
                        if (enumerateFileSystemInfo is FileInfo)
                        {
                            haveFile = true;
                            if (entryName.Contains("Dockerfile"))
                            {
                                //logger?.Info($"Find Dockerfile In Package: {mathchEntryName}");
                            }
                            DoCreateEntryFromFile(destination, enumerateFileSystemInfo.FullName, entryName, compressionLevel);
                        }
                        else
                        {
                            DirectoryInfo possiblyEmptyDir = enumerateFileSystemInfo as DirectoryInfo;
                            if (possiblyEmptyDir != null && IsDirEmpty(possiblyEmptyDir))
                                destination.CreateEntry(entryName + s_pathSeperator.ToString());
                        }
                    }

                    if ((includeBaseDirectory & flag))
                    {
                        string str = directoryInfo.Name;
                        destination.CreateEntry(str + s_pathSeperator.ToString());
                    }
                }
                if (!haveFile)
                {
                    throw new Exception("no file was packaged!");
                }
                return outStream.ToArray();
            }
        }

        /// <summary>
        /// 打包文件夹
        /// </summary>
        /// <param name="sourceDirectoryName"></param>
        /// <param name="compressionLevel"></param>
        /// <param name="includeBaseDirectory"></param>
        /// <returns></returns>
        public static byte[] DoCreateFromDirectory(string sourceDirectoryName, CompressionLevel? compressionLevel, bool includeBaseDirectory, List<string> ignoreList = null, Func<int, bool> progress = null/*, Logger logger = null*/)
        {
            //if (ignoreList != null)
            //{
            //    ignoreList.Add("/.git?");
            //}
            var haveFile = false;
            sourceDirectoryName = Path.GetFullPath(sourceDirectoryName);
            using (var outStream = new MemoryStream())
            {
                using (ZipArchive destination = new ZipArchive(outStream, ZipArchiveMode.Create, false))
                {
                    bool flag = true;
                    DirectoryInfo directoryInfo = new DirectoryInfo(sourceDirectoryName);
                    string fullName = directoryInfo.FullName;
                    if (includeBaseDirectory && directoryInfo.Parent != null)
                    {
                        fullName = directoryInfo.Parent.FullName;
                    }
                    var allFile = FindFileDir(sourceDirectoryName);
                    var allFileLength = allFile.Count();
                    var index = 0;
                    foreach (FileSystemInfo enumerateFileSystemInfo in allFile)
                    {
                        index++;
                        var lastProgressNumber = (((long)index * 100 / allFileLength));
                        if (progress != null)
                        {
                            var r = progress.Invoke((int)lastProgressNumber);
                            if (r)
                            {
                                throw new Exception("deploy task was canceled!");
                            }
                        }

                        flag = false;
                        int length = enumerateFileSystemInfo.FullName.Length - fullName.Length;
                        string entryName = EntryFromPath(enumerateFileSystemInfo.FullName, fullName.Length, length);
                        var mathchEntryName = includeBaseDirectory ? entryName.Substring(directoryInfo.Name.Length) : "/" + entryName;
                        if (ignoreList != null && ignoreList.Count > 0)
                        {
                            var haveMatch = false;
                            foreach (var ignorRule in ignoreList)
                            {
                                try
                                {
                                    if (ignorRule.StartsWith("*"))
                                    {
                                        var ignorRule2 = ignorRule.Substring(1);
                                        if (mathchEntryName.EndsWith(ignorRule2))
                                        {
                                            haveMatch = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        var isMatch = Regex.Match(mathchEntryName, ignorRule, RegexOptions.IgnoreCase);//忽略大小写
                                        if (isMatch.Success)
                                        {
                                            haveMatch = true;
                                            break;
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    throw new Exception($"Ignore Rule 【{ignorRule}】 regular error:" + ex.Message);
                                }
                            }

                            if (haveMatch)
                            {
                                continue;
                            }
                        }
                        if (enumerateFileSystemInfo is FileInfo)
                        {
                            if (entryName.Contains("Dockerfile"))
                            {
                                //logger?.Info($"Find Dockerfile In Package: {entryName}");
                            }
                            haveFile = true;
                            DoCreateEntryFromFile(destination, enumerateFileSystemInfo.FullName, entryName, compressionLevel);
                        }
                        else
                        {
                            DirectoryInfo possiblyEmptyDir = enumerateFileSystemInfo as DirectoryInfo;
                            if (possiblyEmptyDir != null && IsDirEmpty(possiblyEmptyDir))
                                destination.CreateEntry(entryName + s_pathSeperator.ToString());
                        }
                    }

                    if ((includeBaseDirectory & flag))
                    {
                        string str = directoryInfo.Name;
                        destination.CreateEntry(str + s_pathSeperator.ToString());
                    }
                }
                if (!haveFile)
                {
                    throw new Exception("no file was packaged!");
                }
                return outStream.ToArray();
            }
        }

        internal static ZipArchiveEntry DoCreateEntryFromFile(
            ZipArchive destination,
            string sourceFileName,
            string entryName,
            CompressionLevel? compressionLevel)
        {
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (sourceFileName == null)
                throw new ArgumentNullException(nameof(sourceFileName));
            if (entryName == null)
                throw new ArgumentNullException(nameof(entryName));
            using (Stream stream = (Stream)File.Open(sourceFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ZipArchiveEntry zipArchiveEntry = compressionLevel.HasValue ? destination.CreateEntry(entryName, compressionLevel.Value) : destination.CreateEntry(entryName);
                DateTime dateTime = File.GetLastWriteTime(sourceFileName);
                if (dateTime.Year < 1980 || dateTime.Year > 2107)
                    dateTime = new DateTime(1980, 1, 1, 0, 0, 0);
                zipArchiveEntry.LastWriteTime = (DateTimeOffset)dateTime;
                using (Stream destination1 = zipArchiveEntry.Open())
                    stream.CopyTo(destination1);
                return zipArchiveEntry;
            }
        }

        public static string EntryFromPath(string entry, int offset, int length)
        {
            for (; length > 0 && ((int)entry[offset] == (int)Path.DirectorySeparatorChar || (int)entry[offset] == (int)Path.AltDirectorySeparatorChar); --length)
                ++offset;
            if (length == 0)
                return string.Empty;
            char[] charArray = entry.ToCharArray(offset, length);
            for (int index = 0; index < charArray.Length; ++index)
            {
                if ((int)charArray[index] == (int)Path.DirectorySeparatorChar || (int)charArray[index] == (int)Path.AltDirectorySeparatorChar)
                    charArray[index] = s_pathSeperator;
            }
            return new string(charArray);
        }

        private static bool IsDirEmpty(DirectoryInfo possiblyEmptyDir)
        {
            using (IEnumerator<FileSystemInfo> enumerator = possiblyEmptyDir.EnumerateFileSystemInfos("*", SearchOption.AllDirectories).GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    FileSystemInfo current = enumerator.Current;
                    return false;
                }
            }
            return true;
        }

        private static bool ZipDirectory(string zipFileName = null, byte[] btDatas = null)
        {
            if (string.IsNullOrWhiteSpace(zipFileName) || btDatas == null || btDatas.Length == 0)
            {
                throw new ArgumentNullException("缺少有效的传入参数！");
            }
            try
            {
                using (System.IO.FileStream fs = new FileStream(zipFileName, FileMode.Create))
                {
                    fs.Write(btDatas, 0, btDatas.Length);
                    fs.Flush();
                    fs.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new DeployBaseException(ex.Message, ex);
            }
        }

        /// <summary>
        /// 解压Zip文件到指定文件夹
        /// </summary>
        /// <param name="zippedFileName">Zip文件路径全名</param>
        /// <param name="destinationPathName">目标路径</param>
        /// <returns></returns>
        public static bool UnZipToDirectory(string zippedFileName = null, string destinationPathName = null)
        {
            if (string.IsNullOrWhiteSpace(zippedFileName) || string.IsNullOrWhiteSpace(destinationPathName))
            {
                throw new ArgumentNullException("缺少有效的传入参数！");
            }
            try
            {
                ZipFile.ExtractToDirectory(zippedFileName, destinationPathName);
                
                return true;
            }
            catch (Exception ex)
            {
                throw new DeployBaseException(ex.Message, ex);
            }
        }

        ///// <summary>
        ///// 获取顶层文件夹
        ///// </summary>
        ///// <param name="cDirPath"></param>
        ///// <returns></returns>
        //private static string GetTopDirName(string cDirPath)
        //{

        //}
        public static void CopyDirectory(string srcPath, string destPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists($"{destPath}\\{i.Name}"))
                        {
                            Directory.CreateDirectory($"{destPath}\\{i.Name}");   //目标目录下不存在此文件夹即创建子文件夹
                        }
                        CopyDirectory(i.FullName, $"{destPath}\\{i.Name}");    //递归调用复制子文件夹
                    }
                    else
                    {
                        File.Copy(i.FullName, destPath + "\\" + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
