using System.IO;
using GsUpdater.Framework.FeedReader;
using GsUpdater.Framework.Sources;
using GsUpdater.Framework.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GsUpdater.Framework.Test.Tasks
{
    [TestClass()]
    public class FileUpdateTaskTest
    {
        private const string FeedUrl = "http://localhost/TestFeed.xml";

        private IUpdateTask _ft;

        [TestMethod()]
        public void PrepareTest()
        {
            var ws = new WebSource();
            _ft = new AppcastReader().Read(ws.GetUpdatesFeed(FeedUrl));
            _ft.Prepare(ws);

            Assert.IsTrue(File.Exists(_ft.PathToZippedUpdate));
        }

        [TestCleanup()]
        public void AfterPrepare()
        {
            _ft.Clear();
        }

    }
}