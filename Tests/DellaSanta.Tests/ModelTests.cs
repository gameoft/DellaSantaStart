using System;
using System.IO;
using DellaSanta.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DellaSanta.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Parse_docx_File()
        {
            //Arrange
            string filename = @"C:\dev\2017\DellaSanta_Test\DellaSanta.Web\uploads\gText Test Questions 2017.docx";
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

            // Create a byte array of file stream length
            byte[] ImageData = new byte[fs.Length];

            DocxParser.Parse(ImageData, Convert.ToInt32(fs.Length));

        }
    }
}
