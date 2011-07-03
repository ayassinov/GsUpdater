using System;
using GsUpdater.Framework.Sources;

namespace GsUpdater.Framework.Tasks
{
    public interface IUpdateTask
    {
        string PathToZippedUpdate { get; }
        string Description { get; set; }
        Version FileVersion { get; set; }
        string RemotePath { get; set; }
        string Checksum { get; set; }
        long FileLength { get; set; }
        string Title { get; set; }

        bool Prepare(IUpdateSource source);
        void Clear();
    }
}