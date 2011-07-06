using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace UpdateCreator.Utils
{
    public class ZipFileUil
    {
        public static void Compress(string[] files, string destination)
        {
            using (ZipFile zip = new ZipFile())
            {
                foreach (var file in files)
                {
                    zip.AddFile(file, string.Empty);
                }

                zip.Save(destination);
            }
        }

        public static void Compress(string sourceFolder, IEnumerable<string> files, string destination)
        {
            using (ZipFile zip = new ZipFile())
            {
                foreach (var file in files)
                {
                    zip.AddFile(Path.Combine(sourceFolder, file), string.Empty);
                }

                zip.Save(destination);
            }
        }
    }
}