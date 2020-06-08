using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechScan.Tool.U8.ServiceDeploy.Base
{
    public class Class1
    {
    }
    /// <summary>
    /// 所有文件打包，暂时无用
    /// </summary>
    public static class ZipHelpertest
    {
        public static void ZipFiles(string path, IEnumerable<string> files,
               CompressionOption compressionLevel = CompressionOption.Normal)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                ZipHelpertest.ZipFilesToStream(fileStream, files, compressionLevel);
            }
        }

        public static byte[] ZipFilesToByteArray(IEnumerable<string> files,
               CompressionOption compressionLevel = CompressionOption.Normal)
        {
            byte[] zipBytes = default(byte[]);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                ZipHelpertest.ZipFilesToStream(memoryStream, files, compressionLevel);
                memoryStream.Flush();
                zipBytes = memoryStream.ToArray();
            }

            return zipBytes;
        }

        public static void Unzip(string zipPath, string baseFolder)
        {
            //using (FileStream fileStream = new FileStream(zipPath, FileMode.Open))
            //{
            //    ZipHelpertest.UnzipFilesFromStream(fileStream, baseFolder);
            //}

            ZipFile.ExtractToDirectory(zipPath, baseFolder);
        }

        public static void UnzipFromByteArray(byte[] zipData, string baseFolder)
        {
            using (MemoryStream memoryStream = new MemoryStream(zipData))
            {
                ZipHelpertest.UnzipFilesFromStream(memoryStream, baseFolder);
            }
        }

        private static void ZipFilesToStream(Stream destination,
                IEnumerable<string> files, CompressionOption compressionLevel)
        {
            using (Package package = Package.Open(destination, FileMode.Create))
            {
                foreach (string path in files)
                {
                    // fix for white spaces in file names (by ErrCode)
                    Uri fileUri = PackUriHelper.CreatePartUri(new Uri(@"/" +
                                  Path.GetFileName(path), UriKind.Relative));
                    string contentType = @"data/" + ZipHelpertest.GetFileExtentionName(path);

                    using (Stream zipStream =
                            package.CreatePart(fileUri, contentType, compressionLevel).GetStream())
                    {
                        using (FileStream fileStream = new FileStream(path, FileMode.Open))
                        {
                            fileStream.CopyTo(zipStream);
                        }
                    }
                }
            }
        }

        private static void UnzipFilesFromStream(Stream source, string baseFolder)
        {
            if (!Directory.Exists(baseFolder))
            {
                Directory.CreateDirectory(baseFolder);
            }

            using (Package package = Package.Open(source, FileMode.Open))
            {
                foreach (PackagePart zipPart in package.GetParts())
                {
                    // fix for white spaces in file names (by ErrCode)
                    string path = Path.Combine(baseFolder,
                         Uri.UnescapeDataString(zipPart.Uri.ToString()).Substring(1));

                    using (Stream zipStream = zipPart.GetStream())
                    {
                        using (FileStream fileStream = new FileStream(path, FileMode.Create))
                        {
                            zipStream.CopyTo(fileStream);
                        }
                    }
                }
            }
        }

        private static string GetFileExtentionName(string path)
        {
            string extention = Path.GetExtension(path);
            if (!string.IsNullOrWhiteSpace(extention) && extention.StartsWith("."))
            {
                extention = extention.Substring(1);
            }

            return extention;
        }
    }
}
