using System.IO;
using GsUpdater.Framework.Sources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GsUpdater.Framework.Test.Source
{
    [TestClass()]
    public class WebSourceTest
    {
        private const string FeedUrl = "http://localhost/TestFeed.xml";
        private const string FileUrl = "file:///D:/Code/Perso/.NET-Auto-Update/docs/SampleApp1.2.zip";
        private const string Tempfolder = @"d:\test.zip";

        [TestMethod]
        public void GetUpdatesFeed()
        {
            const string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
<rss version=""2.0"" xmlns:appcast=""http://www.adobe.com/xml-namespaces/appcast/1.0"">
  <channel>
    <title>Test Update Feed</title>";

            var ws = new WebSource();
            var str = ws.GetUpdatesFeed(FeedUrl);

            Assert.AreEqual(expected, str.Substring(0, expected.Length));
        }

        [TestMethod]
        public void DownloadUpdate()
        {
            var ws = new WebSource();
            ws.DownloadUpdate(FileUrl, Tempfolder);
            Assert.IsTrue(File.Exists(Tempfolder));
        }
    }
}