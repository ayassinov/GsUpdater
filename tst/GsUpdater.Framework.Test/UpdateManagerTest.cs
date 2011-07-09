using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GsUpdater.Framework.Test
{
    [TestClass]
    public class UpdateManagerTest
    {
        private const string FeedUrl = "http://localhost/TestFeed.xml";

        [TestMethod]
        public void DoUpdateTest()
        {
            var isUpdateExist = UpdateManager.Instance.CheckForUpdate(FeedUrl, new Version("5.0"));
            Assert.IsTrue(isUpdateExist);
            UpdateManager.Instance.DoUpdate();
            //todo check this test
            //Assert.IsTrue(true);
        }

        [TestCleanup()]
        public void AfterPrepare()
        {
            UpdateManager.Instance.Clear();
        }
    }
}