using System;
using System.IO;
using System.Security.Cryptography;

namespace GsUpdater.Framework.Utils
{
    public class FileChecksum
    {
        public static string GetSHA256Checksum(string tempfilepath)
        {
            using (FileStream stream = File.OpenRead(tempfilepath))
            {
                SHA256Managed sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }
    }
}