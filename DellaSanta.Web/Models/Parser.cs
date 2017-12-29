using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DellaSanta.Web.Models
{
    public class DocxParser
    {

        public static void Parse(byte[] FileBytes, int ContentLength)
        {
            try
            {
                using (MemoryStream upFile = new MemoryStream())
                {
                    upFile.Write(FileBytes, 0, ContentLength);


                    // Open the package with read access.
                    //var pippo = data.GetType();
                    // Open a WordprocessingDocument for editing using the filepath.
                    WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(upFile, true);

                    // Assign a reference to the existing document body.
                    Body body = wordprocessingDocument.MainDocumentPart.Document.Body;

                    var pippo = body.ChildElements;

                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }

        }

    }
}