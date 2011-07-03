using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;

// Used for the named pipes implementation

namespace GsUpdater.Framework
{
    /// <summary>
    /// Starts the cold update process by extracting the updater app from the library's resources,
    /// passing it all the data it needs and terminating the current application
    /// </summary>
    internal class UpdateStarter
    {
        private readonly string _updaterPath;
        private readonly string _sourcePath;
        private readonly string _applicationPath;

        public UpdateStarter(string pathWhereUpdateExeShouldBeCreated, string sourcePath, string applicationPath)
        {
            _updaterPath = pathWhereUpdateExeShouldBeCreated;
            _sourcePath = sourcePath;
            _applicationPath = applicationPath;
        }

        public bool Start()
        {
            ExtractUpdaterFromResource();

            using (NamedPipeServerStream pipeServer = new NamedPipeServerStream("gsupdater", PipeDirection.Out))
            {
                ExecuteUpdater();

                pipeServer.WaitForConnection();

                try
                {
                    using (StreamWriter sw = new StreamWriter(pipeServer))
                    {
                        sw.AutoFlush = true;
                        sw.WriteLine(_sourcePath);
                        sw.WriteLine(_applicationPath);
                    }
                }
                catch (IOException e)
                {
                    return false;
                }
                //return true;
            }

            Environment.Exit(0);
            return true;
        }

        private void ExecuteUpdater()
        {
            try
            {
                var process = new Process { StartInfo = { FileName = _updaterPath } }; // file to executing
                process.Start();
            }
            catch (Win32Exception)
            {
                // Person denied UAC escallation
            }
        }

        private void ExtractUpdaterFromResource()
        {
            //store the updater temporarily in the designated folder            
            using (var writer = new BinaryWriter(File.Open(_updaterPath, FileMode.Create)))
                writer.Write(Resources.Updater);
        }
    }
}