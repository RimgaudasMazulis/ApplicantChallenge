using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApplicantChallenge.Tests
{
    [TestClass]
    public class PyramidTests
    {
        [TestMethod]
        public void Should_Return_File_Is_Read()
        {
            String filePath = "/App_Data/TestPyramid.txt";
            String[] fileLines = { "4 5 2 3", "1 5 9", "8 9", "1" };
            var data = FileReader.ReadFile(filePath).ToArray();
            CollectionAssert.AreEqual(fileLines, data);
        }
    }
}
