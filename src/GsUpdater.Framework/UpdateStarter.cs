using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Windows.Forms;

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

        public void Start()
        {
            try
            {
                ExtractUpdaterFromResource();

                using (NamedPipeServerStream pipeServer = new NamedPipeServerStream("gsupdater", PipeDirection.Out))
                {
                    ExecuteUpdater();

                    pipeServer.WaitForConnection();

                    using (StreamWriter sw = new StreamWriter(pipeServer))
                    {
                        sw.AutoFlush = true;
                        sw.WriteLine(_sourcePath);
                        sw.WriteLine(_applicationPath);
                    }

                    Environment.Exit(0);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(@"Une erreur s'est produite lors du démarrage de la mise à jour de l'application"
                         + Environment.NewLine
                         + @"Detail du message :"
                         + Environment.NewLine
                         + e.Message);
            }
        }

        private void ExecuteUpdater()
        {
            var process = new Process
                              {
                                  StartInfo =
                                      {
                                          //WorkingDirectory = Path.GetDirectoryName(_updaterPath),
                                          FileName = _updaterPath
                                      }
                              };
            process.Start();
        }

        private void ExtractUpdaterFromResource()
        {
            using (var writer = new BinaryWriter(File.Open(_updaterPath, FileMode.Create)))
                writer.Write(Resources.Updater);
        }
    }
}