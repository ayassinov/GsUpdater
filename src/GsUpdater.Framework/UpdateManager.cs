using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using GsUpdater.Framework.FeedReader;
using GsUpdater.Framework.Sources;
using GsUpdater.Framework.Tasks;

namespace GsUpdater.Framework
{
    public class UpdateManager
    {
        public string TempPath { get; private set; }
        public string UrlFeed { get; set; }
        public UpdatingState State { get; set; }

        private string ApplicationPath { get; set; }
        private string UpdaterTempPath { get; set; }

        public IUpdateTask CurrentUpdate { get; private set; }
        private IUpdateSource CurrentSourceUpdate { get; set; }

        private Version CurrentVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }

        private UpdateManager()
        {
            State = UpdatingState.NotChecked;
            ApplicationPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        }

        private static readonly UpdateManager _instance = new UpdateManager();

        public static UpdateManager Instance
        {
            get { return _instance; }
        }

        public bool CheckForUpdate()
        {
            try
            {
                CurrentSourceUpdate = new WebSource();
                CurrentUpdate = new AppcastReader().Read(CurrentSourceUpdate.GetUpdatesFeed(UrlFeed));
                int cpr = CurrentUpdate.FileVersion.CompareTo(CurrentVersion);
                return cpr > 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(@"Une erreur s'est produite lors de la vérification des mises à jours."
                    + Environment.NewLine
                    + @"Detail de l'erreur :"
                    + Environment.NewLine
                    + e.Message);
                return false;
            }
        }

        public bool CheckForUpdate(string feedUrl)
        {
            UrlFeed = feedUrl;
            return CheckForUpdate();
        }

        private bool PrepareUpdate()
        {
            try
            {
                if (CurrentUpdate == null)
                    throw new Exception("Aucune mise à jour n'a été tourvé.");

                TempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

                return CurrentUpdate.Prepare(CurrentSourceUpdate);
            }
            catch (Exception e)
            {
                MessageBox.Show(@"Une erreur s'est produite lors de préparation de la mise à jour."
                 + Environment.NewLine
                 + @"Detail de l'erreur :"
                 + Environment.NewLine
                 + Environment.NewLine
                 + e.Message);
                return false;
            }
        }

        public void DoUpdate()
        {
            if (!PrepareUpdate())
                return;

            UpdaterTempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            if (!Directory.Exists(UpdaterTempPath))
                Directory.CreateDirectory(UpdaterTempPath);

            var updStarter = new UpdateStarter(
                Path.Combine(UpdaterTempPath, "GsUpdater.exe"),
                TempPath,
                ApplicationPath);

            updStarter.Start();

            State = UpdatingState.Updated;
        }

        public void Clear()
        {
            if (CurrentUpdate == null)
                return;

            CurrentUpdate.Clear();
        }

        public void Clear(string[] args)
        {
            try
            {
                if (args != null && args.Length > 0)
                {
                    var folders = args[0].Split(';');

                    foreach (var folder in folders)
                    {
                        if (Directory.Exists(folder))
                            Directory.Delete(folder, true);
                    }
                }
            }
            catch (Exception)
            {
                //silent clearing
            }
        }
    }
}