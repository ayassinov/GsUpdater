namespace GsUpdater.Framework.Sources
{
    public interface IUpdateSource
    {
        string GetUpdatesFeed(string feedUrl);

        bool DownloadUpdate(string remoteUrl, string tempFolder);
    }
}