using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Windows.Forms;

namespace GsUpdater.Updater
{
    internal static class AppStart
    {
        private static void Main()
        {

            try
            {
                using (var pipeClient = new NamedPipeClientStream(".", "gsupdater", PipeDirection.In))
                {
                    pipeClient.Connect();
                    var parameters = new List<string>();
                    using (var sr = new StreamReader(pipeClient))
                    {
                        string temp;
                        while ((temp = sr.ReadLine()) != null)
                        {
                            parameters.Add(temp);
                        }
                    }

                    if (parameters.Count == 0)
                        throw new Exception("No parameters");

                    var filesToCopy = Directory.GetFiles(parameters[0]); //source path for file to update 
                    var appPath = Path.GetDirectoryName(parameters[1]);
                    foreach (var file in filesToCopy)
                    {
                        File.Copy(file, Path.Combine(appPath, Path.GetFileName(file)), true); //application directory
                    }

                    var process = new Process { StartInfo = { FileName = parameters[1] } }; // file to executing
                    process.Start();
                }
            }
            catch
            {
                //supressing catch because if at any point we get an error the update has failed
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}