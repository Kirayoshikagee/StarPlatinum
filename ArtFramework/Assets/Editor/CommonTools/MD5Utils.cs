using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    public class MD5Utils
    {
        
        public static Byte[] CreateMD5(Byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(data);
            }
        }

        
        public static string CreateMD5(string str)
        {
            byte[] inbytes = Encoding.UTF8.GetBytes(str);
            return FormatMD5(inbytes);
        }

        
        public static string BuildFileMd5(string filePath)
        {
            string filemd5 = string.Empty;
            try
            {
                using (var fileStream = File.OpenRead(filePath))
                {
                    var md5 = MD5.Create();
                    var fileMD5Bytes = md5.ComputeHash(fileStream);               
                    filemd5 = FormatMD5(fileMD5Bytes);
                }
            }
            catch (Exception ex)
            {
                DebugUtil.LogError("build file md5 fail");
            }
            return filemd5;
        }

        public static string FormatMD5(Byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", "").ToLower();
        }
    }
}
