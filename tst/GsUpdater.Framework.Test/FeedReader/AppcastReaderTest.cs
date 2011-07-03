using System;
using GsUpdater.Framework.FeedReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GsUpdater.Framework.Tasks;

namespace GsUpdater.Framework.Test.FeedReader
{

    [TestClass()]
    public class AppcastReaderTest
    {
        private const string Data = @"<?xml version=""1.0"" encoding=""utf-8""?>
    <rss version=""2.0"" xmlns:appcast=""http://www.adobe.com/xml-namespaces/appcast/1.0"">
     <channel>
        <title>Test Update Feed</title>
        <link>http://www.linkToapplication.com</link>
        <description>Sample application that demonstrates the autoupdate capabilities</description>
            <item>
                <title>SampleApp</title>
                <description>WMA Support and other minor bug fixes</description>
                <pubDate>Sunday, 27 December 2009 00:35:00 GMT</pubDate>
                <enclosure 
	                    url=""file:///D:/Code/Perso/.NET-Auto-Update/docs/SampleApp1.2.zip"" 
	                    length=""17574""
	                    type=""application/octet-stream"" 
	                    version=""1.2""
                        checksum=""7F34E129F80CE5B496B7799ABD7351D50AB1943FC173D4992D9AF63E85C77E38""/>
            </item>
     </channel>
    </rss>";

        readonly IUpdateTask _expected = new FileUpdateTask()
                                       {
                                           Checksum = "7F34E129F80CE5B496B7799ABD7351D50AB1943FC173D4992D9AF63E85C77E38",
                                           Description = "WMA Support and other minor bug fixes",
                                           FileLength = Convert.ToInt64("17574"),
                                           FileVersion = new Version("1.2"),
                                           RemotePath = "file:///D:/Code/Perso/.NET-Auto-Update/docs/SampleApp1.2.zip",
                                           Title = "SampleApp"
                                       };

        [TestMethod()]
        public void ReadTest()
        {
            IUpdateTask actual = new AppcastReader().Read(Data);
            Assert.AreEqual(_expected.Checksum, actual.Checksum);
            Assert.AreEqual(_expected.Description, actual.Description);
            Assert.AreEqual(_expected.FileLength, actual.FileLength);
            Assert.AreEqual(_expected.FileVersion, actual.FileVersion);
            Assert.AreEqual(_expected.RemotePath, actual.RemotePath);
            Assert.AreEqual(_expected.Title, actual.Title);
        }
    }
}
