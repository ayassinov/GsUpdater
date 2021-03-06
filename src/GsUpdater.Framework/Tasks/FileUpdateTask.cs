using System;
using System.IO;
using GsUpdater.Framework.Sources;
using GsUpdater.Framework.Utils;

namespace GsUpdater.Framework.Tasks
{
    public class FileUpdateTask : IUpdateTask
    {
        public string PathToZippedUpdate
        {
            get { return Path.Combine(UpdateManager.Instance.TempPath, "gsupdatefile.zip"); }
        }

        public string Description { get; set; }

        public Version FileVersion { get; set; }

        public string RemotePath { get; set; }

        public string Checksum { get; set; }

        public long FileLength { get; set; }

        public string Title { get; set; }

        public bool Prepare(IUpdateSource source)
        {
            try
            {
                if (!Directory.Exists(UpdateManager.Instance.TempPath))
                    Directory.CreateDirectory(UpdateManager.Instance.TempPath);

                //download zip file to tmp folder
                if (!source.DownloadUpdate(RemotePath, PathToZippedUpdate))
                    throw new Exception("Impossible de t�l�charger la mise � jour");

                //check file checksum  
                if (!string.IsNullOrEmpty(Checksum))
                {
                    string checksum = FileChecksum.GetSHA256Checksum(PathToZippedUpdate);
                    if (!checksum.Equals(Checksum))
                        throw new Exception("Le fichier de mise � jour t�l�charg� n'est pas valide");
                }

                //extract file to temp folder and delete the zip file.
                ZipFileUil.Extract(PathToZippedUpdate, UpdateManager.Instance.TempPath);

                //delete the zipped file
                if (File.Exists(PathToZippedUpdate))
                    File.Delete(PathToZippedUpdate);
            }
            catch (Exception)
            {
                Clear();
                throw;
            }

            return true;
        }

        public void Clear()
        {
            if (string.IsNullOrEmpty(UpdateManager.Instance.TempPath))
                return;

            if (Directory.Exists(UpdateManager.Instance.TempPath))
                Directory.Delete(UpdateManager.Instance.TempPath, true);
        }
    }
}