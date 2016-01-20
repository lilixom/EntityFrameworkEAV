using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TAP.FileService.Helper
{
    public static class FileHelper
    {
        public static string ByteArrayToHexString(byte[] buf)
        {
            string str = "";
            if (buf != null)
            {
                for (int i = 0; i < buf.Length; i = (int)(i + 1))
                {
                    str = str + ((byte)buf[i]).ToString("X2");
                }
            }
            return str;
        }

        public static long GetFileSize(string _FileFullPath)
        {
            if (!File.Exists(_FileFullPath))
            {
                return 0;
            }
            try
            {
                FileInfo info = new FileInfo(_FileFullPath);
                return info.Length;
            }
            catch
            {
            }
            return 0;
        }

        public static byte[] HashData(Stream stream, string algName)
        {
            HashAlgorithm algorithm;
            if (algName == null)
            {
                throw new ArgumentNullException("algName 不能为 null");
            }
            if (string.Compare(algName, "sha1", true) == 0)
            {
                algorithm = SHA1.Create();
            }
            else
            {
                if (string.Compare(algName, "md5", true) != 0)
                {
                    throw new Exception("algName 只能使用 sha1 或 md5");
                }
                algorithm = MD5.Create();
            }
            return algorithm.ComputeHash(stream);
        }

        public static string HashFile(Stream stream, string algName)
        {
            return ByteArrayToHexString(HashData(stream, algName));
        }

        public static string HashFile(string fileName, string algName)
        {
            if (!File.Exists(fileName))
            {
                return string.Empty;
            }
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                return ByteArrayToHexString(HashData(stream, algName));
            }
        }

        public static string MD5File(Stream stream)
        {
            return HashFile(stream, "md5");
        }

        public static string MD5File(string fileName)
        {
            return HashFile(fileName, "md5");
        }
    }
}