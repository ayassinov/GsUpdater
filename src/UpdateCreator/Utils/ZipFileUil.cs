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
                    zip.AddFile(file);
                }

                zip.Save(destination);
            }
        }
    }
}