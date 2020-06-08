/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   CopyHelper.cs
* 功能描述： 文件拷贝支持类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-24 17:12:31
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-24 17:12:31		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TechScan.Tool.U8.ServiceDeploy.Base.Exceptions;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Utils
{
    /// <summary>
    /// 文件拷贝支持类
    /// </summary>
    public static class CopyHelper
    {
        #region Methods

        /// <summary>
        /// 检查某个路径是否是文件目录
        /// </summary>
        /// <param name="cFullPathOrFileName"></param>
        /// <returns></returns>
        public static bool PathIsDirectory(string cFullPathOrFileName)
        {
            if (string.IsNullOrEmpty(cFullPathOrFileName))
            {
                throw new ArgumentNullException("未传入有效的文件或者路径名！");
            }
            System.IO.FileInfo info = new System.IO.FileInfo(cFullPathOrFileName);
            if ((info.Attributes & System.IO.FileAttributes.Directory) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// copies if the files and folders are existent in referenced directory
        /// </summary>
        /// <param name="srcDir">source directory</param>
        /// <param name="dstDir">destination directory</param>
        /// <param name="refDir">reference directory</param>
        /// <param name="copySubDirs">copy sub directories</param>
        public static void DirectoryCopyWithRef(string srcDir, string dstDir, string refDir, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(refDir);

            if (!dir.Exists)
                throw new DirectoryNotFoundException("Reference directory does not exist or could not be found: " + refDir);

            if (!Directory.Exists(dstDir))
                Directory.CreateDirectory(dstDir);

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string srcFile = Path.Combine(srcDir, file.Name);
                string dstFile = Path.Combine(dstDir, file.Name);
                if (File.Exists(srcFile))
                {
                    File.Copy(srcFile, dstFile, true);
                }
            }

            if (copySubDirs)
            {
                DirectoryInfo[] subDirs = dir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirs)
                {
                    string srcDirSub = Path.Combine(srcDir, subDir.Name);
                    string dstDirSub = Path.Combine(dstDir, subDir.Name);
                    DirectoryCopyWithRef(srcDirSub, dstDirSub, subDir.FullName, true);
                }
            }
        }

        /// <summary>
        /// copies source directory to the destionation directory
        /// </summary>
        /// <param name="srcDir">source directory</param>
        /// <param name="dstDir">destination directory</param>
        /// <param name="copySubDirs">copy sub directories</param>
        public static void DirectoryCopy(string srcDir, string dstDir, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(srcDir);

            if (!dir.Exists)
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + srcDir);

            if (!Directory.Exists(dstDir))
            {
                try
                {
                    Directory.CreateDirectory(dstDir);
                }
                catch (Exception ex)
                {
                    throw new Exception($"CreateDirectory:{dstDir} Fail:{ex.Message}");
                }
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string dstFile = Path.Combine(dstDir, file.Name);
                try
                {
                    file.CopyTo(dstFile, true);
                }
                catch (Exception ex)
                {
                    throw new Exception($"from:{file.FullName} copy to {dstFile} Fail:{ex.Message}");
                }
            }

            if (copySubDirs)
            {
                DirectoryInfo[] subDirs = dir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirs)
                {
                    string dstDirSub = Path.Combine(dstDir, subDir.Name);
                    DirectoryCopy(subDir.FullName, dstDirSub, copySubDirs);
                }
            }
        }

        public static void DirectoryCopy(string srcDir, string dstDir, bool copySubDirs, string parentDir, string parentName, List<string> backignoreUpList)
        {
            if (backignoreUpList == null || !backignoreUpList.Any())
            {
                DirectoryCopy(srcDir, dstDir, copySubDirs);
                return;
            }

            if (string.IsNullOrEmpty(parentDir))
            {
                throw new DirectoryNotFoundException("parentDir is empty ");
            }

            DirectoryInfo dir = new DirectoryInfo(srcDir);

            if (!dir.Exists)
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + srcDir);

            if (!Directory.Exists(dstDir))
            {
                try
                {
                    Directory.CreateDirectory(dstDir);
                }
                catch (Exception ex)
                {
                    throw new Exception($"CreateDirectory:{dstDir} Fail:{ex.Message}");
                }
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                int length = file.FullName.Length - parentDir.Length;
                string entryName = EntryFromPath(file.FullName, parentDir.Length, length);
                string matchEntryName = entryName.Substring(parentName.Length);
                if (IsMacthIgnore(entryName, backignoreUpList)) continue;
                string dstFile = Path.Combine(dstDir, file.Name);
                try
                {
                    file.CopyTo(dstFile, true);
                }
                catch (Exception ex)
                {
                    throw new Exception($"from:{file.FullName} copy to {dstFile} Fail:{ex.Message}");
                }
            }

            if (copySubDirs)
            {
                DirectoryInfo[] subDirs = dir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirs)
                {
                    string dstDirSub = Path.Combine(dstDir, subDir.Name);
                    int length = subDir.FullName.Length - parentDir.Length;
                    string entryName = EntryFromPath(subDir.FullName, parentDir.Length, length);
                    if (IsMacthIgnore(entryName, backignoreUpList)) continue;
                    DirectoryCopy(subDir.FullName, dstDirSub, copySubDirs, parentDir, parentName, backignoreUpList);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="backignoreUpList"></param>
        /// <returns></returns>
        private static bool IsMacthIgnore(string entryName, List<string> backignoreUpList)
        {
            var haveMatch = false;
            foreach (var ignorRule in backignoreUpList)
            {
                try
                {
                    if (ignorRule.StartsWith("*"))
                    {
                        var ignorRule2 = ignorRule.Substring(1);
                        if (entryName.EndsWith(ignorRule2))
                        {
                            haveMatch = true;
                            break;
                        }
                    }
                    else
                    {
                        var isMatch = Regex.Match(entryName, ignorRule, RegexOptions.IgnoreCase);//忽略大小写
                        if (isMatch.Success)
                        {
                            haveMatch = true;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return haveMatch;
        }
        private static string EntryFromPath(string entry, int offset, int length)
        {
            for (; length > 0 && ((int)entry[offset] == (int)Path.DirectorySeparatorChar || (int)entry[offset] == (int)Path.AltDirectorySeparatorChar); --length)
                ++offset;
            if (length == 0)
                return string.Empty;
            char[] charArray = entry.ToCharArray(offset, length);
            for (int index = 0; index < charArray.Length; ++index)
            {
                if ((int)charArray[index] == (int)Path.DirectorySeparatorChar || (int)charArray[index] == (int)Path.AltDirectorySeparatorChar)
                    charArray[index] = '/';
            }
            return new string(charArray);
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="cSourceFileName">源文件名称</param>
        /// <param name="cDestDir">目标目录名称</param>
        /// <param name="bOverWrite">是否覆盖原有文件</param>
        public static string FileCopy(string cSourceFileName, string cDestDir, bool bOverWrite = true)
        {
            if (!System.IO.File.Exists(cSourceFileName))
            {
                throw new IISDeployRuntimeException("文件复制失败！源文件不存在！");
            }
            var cDestFileName = System.IO.Path.Combine(cDestDir, Path.GetFileName(cSourceFileName));
            File.Copy(cSourceFileName, cDestFileName, bOverWrite);

            return cDestFileName;
        }

        /// <summary>
        /// 删除目录下所有文件
        /// </summary>
        /// <param name="cDirPath">目录</param>
        /// <param name="bDeleteDirSelf">是否删除目录本身</param>
        public static void DeleteDirAllFiles(string cDirPath,bool bDeleteDirSelf)
        {
            try
            {
                //去除文件夹和子文件的只读属性
                //去除文件夹的只读属性
                System.IO.DirectoryInfo fileInfo = new DirectoryInfo(cDirPath);
                fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
                //去除文件的只读属性
                System.IO.File.SetAttributes(cDirPath, System.IO.FileAttributes.Normal);

                //判断文件夹是否还存在
                if (Directory.Exists(cDirPath))
                {
                    foreach (string f in Directory.GetFileSystemEntries(cDirPath))
                    {
                        if (File.Exists(f))
                        {
                            //如果有子文件删除文件
                            File.Delete(f);
                        }
                        else
                        {
                            //循环递归删除子文件夹
                            DeleteDirAllFiles(f,true);
                        }
                    }
                    if (bDeleteDirSelf)
                    {
                        //删除空文件夹
                        Directory.Delete(cDirPath);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
