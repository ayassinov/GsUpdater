using System.Collections.Generic;
using GsUpdater.Framework.Tasks;

namespace GsUpdater.Framework.FeedReader
{
    public interface IUpdateFeedReader
    {
        IUpdateTask Read(string feed);
    }
}