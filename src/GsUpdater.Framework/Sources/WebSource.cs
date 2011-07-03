using System;
using System.IO;
using System.Net;

namespace GsUpdater.Framework.Sources
{
    public class WebSource : IUpdateSource
    {
        public string GetUpdatesFeed(string feedUrl)
        {
            string data;

            var request = WebRequest.Create(feedUrl);
            request.Method = "GET";
            using (var response = request.GetResponse())
            {
                if (response == null)
                    return null;

                using (var reader = new StreamReader(response.GetResponseStream(), true))
                {
                    data = reader.ReadToEnd();
                }
            }

            return data;
        }

        public bool DownloadUpdate(string remoteUrl, string tempFolder)
        {
            var webClient = new WebClient();
            try
            {
                if (File.Exists(tempFolder))
                    File.Delete(tempFolder);

                webClient.DownloadFile(remoteUrl, tempFolder);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}