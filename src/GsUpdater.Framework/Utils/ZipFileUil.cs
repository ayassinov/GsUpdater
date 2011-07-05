using System;
using Ionic.Zip;

namespace GsUpdater.Framework.Utils
{
    public class ZipFileUil
    {
        public static void Extract(string pathtozip, string unpackDirectory)
        {
            try
            {
                using (ZipFile zip1 = ZipFile.Read(pathtozip))
                {
                    foreach (ZipEntry e in zip1)
                    {
                        e.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}