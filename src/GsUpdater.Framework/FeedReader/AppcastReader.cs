using System;
using System.Collections.Generic;
using System.Xml;
using GsUpdater.Framework.Sources;
using GsUpdater.Framework.Tasks;

namespace GsUpdater.Framework.FeedReader
{
    public class AppcastReader : IUpdateFeedReader
    {
        public IUpdateTask Read(string feed)
        {
            var doc = new XmlDocument();
            doc.LoadXml(feed);
            
            XmlNode node = doc.SelectSingleNode("/rss/channel/item");
            if (node == null)
                return null;

            IUpdateTask task = new FileUpdateTask();
            task.Title = node["title"].InnerText;
            task.Description = node["description"].InnerText;
            task.RemotePath = node["enclosure"].Attributes["url"].Value;
            task.FileLength = Convert.ToInt64(node["enclosure"].Attributes["length"].Value);
            task.FileVersion = new Version(node["enclosure"].Attributes["version"].InnerText);
            task.Checksum = node["enclosure"].Attributes["checksum"].InnerText;

            return task;
        }
    }
}