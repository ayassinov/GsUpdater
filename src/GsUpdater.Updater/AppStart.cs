using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GsUpdater.Updater
{
    internal static class AppStart
    {
        private static string _appExecutable = string.Empty;
        private static string _args = string.Empty;
        private static List<string> _filesToRollBack = new List<string>();

        static internal ArrayList myProcessArray = new ArrayList();
        private static Process myProcess;

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
                        throw new Exception("Aucun paramètres n'a été trouvé.");



                    var filesToCopy = Directory.GetFiles(parameters[0]); //source path for file to update 
                    var appPath = Path.GetDirectoryName(parameters[1]); // destination path.
                    _appExecutable = parameters[1];

                    if (!PermissionsCheck.HaveWritePermissionsForFolder(appPath))
                        throw new Exception("Vous n'avez pas la persmission d'écriture sur ce repertoire :"
                            + Environment.NewLine
                            + appPath);


                    var backFolder = Path.Combine(appPath, "back");
                    if (!Directory.Exists(backFolder))
                        Directory.CreateDirectory(backFolder);

                    _args = string.Format("{0};{1};{2}", parameters[0], Application.ExecutablePath, backFolder);

                    Thread.Sleep(2000);

                    string filetobackup;
                    string fileName = string.Empty;
                    foreach (var file in filesToCopy)
                    {
                        try
                        {
                            //backup files.
                            fileName = Path.GetFileName(file);
                            filetobackup = Path.Combine(appPath, fileName);
                            if (File.Exists(filetobackup))
                                File.Copy(filetobackup, Path.Combine(backFolder, fileName), true);

                            //copy file.
                            File.Copy(file, Path.Combine(appPath, fileName), true); //application directory

                            //adding file to rollback
                            if (File.Exists(Path.Combine(backFolder, fileName)))
                                _filesToRollBack.Add(Path.Combine(backFolder, fileName));
                        }
                        catch (IOException e)
                        {
                            e.Source = Path.Combine(appPath, fileName);
                            throw;
                        }

                    }
                }
            }
            catch (IOException e)
            {
                string strFile = e.Source;

                var sb = new StringBuilder();
                foreach (Process p in getFileProcesses(strFile))
                {
                    sb.Append(p.ProcessName + Environment.NewLine);
                }

                MessageBox.Show("Une erreur s'est produite lors de la copie du fichier : "
                    + Path.GetFileName(e.Source)
                    + Environment.NewLine
                    + "Detil de l'erreur :"
                    + Environment.NewLine
                    + Environment.NewLine
                    + e.Message
                    + Environment.NewLine
                    + "Liste des processessus : "
                    + Environment.NewLine
                    + sb.ToString());

                RollBack();
            }
            catch (Exception e)
            {
                MessageBox.Show("La mise à jour a echoué, l'application va annuler la mise à jour... "
                    + Environment.NewLine
                    + "Detil de l'erreur :"
                    + Environment.NewLine
                    + Environment.NewLine
                    + e.Message);

                RollBack();
            }
            finally
            {

                LunchApplication();

                Environment.Exit(0);
            }
        }

        private static void RollBack()
        {
            try
            {
                var appPath = Path.GetDirectoryName(_appExecutable);
                foreach (var file in _filesToRollBack)
                {
                    //rolback files.
                    File.Copy(file, Path.Combine(appPath, Path.GetFileName(file)), true);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Une erreur c'est produite lors du rollback des fichiers" + Environment.NewLine + e.Message);
            }
        }

        private static void LunchApplication()
        {
            try
            {
                var process = new Process
                         {
                             StartInfo =
                             {
                                 WorkingDirectory = Path.GetDirectoryName(_appExecutable),
                                 FileName = _appExecutable,
                                 Arguments = _args
                             }

                         };
                process.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show("L'application n'a pas pu être redemarrer, vous devez le faire manuellment."
                                + Environment.NewLine
                                + "Detail de l'erreur :"
                                + Environment.NewLine
                                + Environment.NewLine
                                + e.Message);
            }
        }

        private static ArrayList getFileProcesses(string strFile)
        {
            try
            {
                myProcessArray.Clear();
                Process[] processes = Process.GetProcesses();
                int i = 0;
                for (i = 0; i <= processes.GetUpperBound(0) - 1; i++)
                {
                    myProcess = processes[i];
                    //if (!myProcess.HasExited) //This will cause an "Access is denied" error
                    if (myProcess.Threads.Count > 0)
                    {
                        try
                        {
                            ProcessModuleCollection modules = myProcess.Modules;
                            int j = 0;
                            for (j = 0; j <= modules.Count - 1; j++)
                            {
                                if ((modules[j].FileName.ToLower().CompareTo(strFile.ToLower()) == 0))
                                {
                                    myProcessArray.Add(myProcess);
                                    break;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            //silent exception
                        }
                    }
                }
            }
            catch (Exception)
            {
                //supress exception
            }

            return myProcessArray;
        }
    }
}