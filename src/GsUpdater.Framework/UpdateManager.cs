using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using GsUpdater.Framework.FeedReader;
using GsUpdater.Framework.Sources;
using GsUpdater.Framework.Tasks;

namespace GsUpdater.Framework
{
    public class UpdateManager
    {
        private const string UpdateProcessName = "GsUpdateProcess";

        public string TempPath { get; private set; }
        public string UrlFeed { get; set; }

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
            //State = UpdateProcessState.NotChecked;
            ApplicationPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            //BackupFolder = Path.Combine(Path.GetDirectoryName(ApplicationPath) ?? string.Empty, "Backup");
        }

        private static readonly UpdateManager _instance = new UpdateManager();

        public static UpdateManager Instance
        {
            get { return _instance; }
        }

        public bool CheckForUpdate()
        {
            CurrentSourceUpdate = new WebSource();
            CurrentUpdate = new AppcastReader().Read(CurrentSourceUpdate.GetUpdatesFeed(UrlFeed));
            int cpr = CurrentUpdate.FileVersion.CompareTo(CurrentVersion);
            return cpr > 0;
        }

        public bool CheckForUpdate(string feedUrl)
        {
            UrlFeed = feedUrl;
            return CheckForUpdate();
        }

        private bool PrepareUpdate()
        {
            if (CurrentUpdate == null)
                return false; //todo throw exception, nothing to update.

            //set a new temp folder
            TempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            var isPrepared = CurrentUpdate.Prepare(CurrentSourceUpdate);

            return isPrepared;
        }

        public bool DoUpdate()
        {
            if (!PrepareUpdate())
            {
                //show error.
                return false;
            }

            UpdaterTempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            var parameters = new Dictionary<string, object>();

            // Add some environment variables to the dictionary object which will be passed to the updater
            parameters["AppPath"] = ApplicationPath;
            parameters["TempFolder"] = UpdaterTempPath;
            parameters["SourcePath"] = TempPath;

            if (!Directory.Exists(UpdaterTempPath))
                Directory.CreateDirectory(UpdaterTempPath);

            // Naming it updater.exe seem to trigger the UAC, and we don't want that
            var updStarter = new UpdateStarter(Path.Combine(UpdaterTempPath, "foo.exe"), parameters,
                                               UpdateProcessName);
            bool createdNew;
            using (var _ = new Mutex(true, UpdateProcessName, out createdNew))
            {
                if (!updStarter.Start())
                    return false;

                Environment.Exit(0);
            }


            // State = UpdateProcessState.AppliedSuccessfully;
            //UpdatesToApply.Clear();

            return true;

        }

        public void Clear()
        {
            if (CurrentUpdate == null)
                return;

            CurrentUpdate.Clear();
        }
    }
}