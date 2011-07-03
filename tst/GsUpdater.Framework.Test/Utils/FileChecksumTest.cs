using GsUpdater.Framework.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GsUpdater.Framework.Test.Utils
{
    [TestClass()]
    public class FileChecksumTest
    {
        private const string FilePath = @"d:\test.zip";
        private const string Expected = "7F34E129F80CE5B496B7799ABD7351D50AB1943FC173D4992D9AF63E85C77E38";

        [TestMethod()]
        public void FileCheckSum()
        {
            
            var actual = FileChecksum.GetSHA256Checksum(FilePath);
            Assert.AreEqual(Expected,actual);

        }

    }
}