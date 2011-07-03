using System;
using System.IO;
using GsUpdater.Framework.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GsUpdater.Framework.Test.Utils
{
    [TestClass]
    public class ZipFileUtilTest
    {
        private const string PathToZippedFile = @"D:/Code/Perso/.NET-Auto-Update/docs/SampleApp1.2.zip";
        private const string FileNameInZip = @"leetreveil.AutoUpdate.SampleApp.exe";
        private const int NumberOfFileInZip = 2;
        private string _destinationFolder;

        [TestInitialize]
        public void BeforeExtractTest()
        {
            _destinationFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_destinationFolder);
        }

        [TestMethod]
        public void ExtractTest()
        {
            Assert.IsTrue(Directory.Exists(_destinationFolder));

            int nbrOfFileBeforeExtract = Directory.GetFiles(_destinationFolder).Length;

            ZipFileUil.Extract(PathToZippedFile,_destinationFolder);

            var filesExtracted = Directory.GetFiles(_destinationFolder);

            Assert.AreEqual(nbrOfFileBeforeExtract + NumberOfFileInZip, filesExtracted.Length);
            Assert.IsTrue(Array.Exists(filesExtracted, p => Path.GetFileName(p).CompareTo(FileNameInZip) == 0));
        }

        [TestCleanup()]
        public void AfterExtractTest()
        {
            if(Directory.Exists(_destinationFolder))
                Directory.Delete(_destinationFolder, true);
        }
    }
}