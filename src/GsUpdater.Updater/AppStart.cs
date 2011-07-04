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
                    MessageBox.Show("Connected to pipe");
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
                    MessageBox.Show("copy update, file to count : " + filesToCopy.Length);
                    foreach (var file in filesToCopy)
                    {
                        try
                        {
                            File.Copy(file, Path.Combine(appPath, Path.GetFileName(file)), true); //application directory
                        }
                        catch (IOException e)
                        {
                            //ioexception. //need to restart.
                            MessageBox.Show(e.Message);
                        }

                    }

                    var args = string.Format("{0};{1}", parameters[0], Application.ExecutablePath);
                    MessageBox.Show("Setting parameters : " + args);
                    MessageBox.Show("Starting application" + Environment.NewLine + parameters[1]);
                    var process = new Process
                                      {
                                          StartInfo =
                                              {
                                                  WorkingDirectory = Path.GetDirectoryName(parameters[1]),
                                                  FileName = parameters[1],
                                                  Arguments = args
                                              }

                                      }; // file to executing
                    process.Start();
                    //MessageBox.Show("Application Stated");
                }
            }
            catch (Exception e)
            {
                //supressing catch because if at any point we get an error the update has failed
                MessageBox.Show(e.Message);
            }
            finally
            {
                Environment.Exit(0);
            }
        }
    }
}