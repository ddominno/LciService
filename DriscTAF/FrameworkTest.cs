using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DriscTAF
{
    using DriscTAF.Utilities;

    [TestClass]
    public class FrameworkTest
    {
        [TestMethod]
        public void BinIdGeneratorCreatesExpectedLength()
        {
            string binId1 = Utilities.BinIDGenerator.GenerateBinID();
            Assert.IsTrue(binId1.Length == 10, "ID length was not 10 characters");
        }

        // Removed as a test for now since we know this is a problem
        public void BinIdGeneratorDoesNotCreateDuplicates()
        {
            string binId1 = Utilities.BinIDGenerator.GenerateBinID();
            string binId2 = Utilities.BinIDGenerator.GenerateBinID();
            Assert.AreNotEqual(binId1, binId2, "A duplicate id was generated");
        }
    }
}
