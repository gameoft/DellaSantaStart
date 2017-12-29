using System;
using System.Linq;
using System.IO;
using DellaSanta.Services;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lucene.Net.Index.Memory;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis;
//using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using System.Text;
using DocumentFormat.OpenXml.Drawing;

namespace DellaSanta.Tests
{
    //[TestClass]
    //public class UnitTest1
    //{
    //    [TestMethod]
    //    public void Can_Parse_docx_File()
    //    {
    //        //Arrange
    //        string filename = @"C:\\dev\\2017\\DellaSanta_Test\\DellaSanta.Web\\uploads\\gText Test Questions 2017.docx";
    //        //FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
    //        // Create a byte array of file stream length
    //        //var paragraphs = DocxProcessingService.ParseUploadedDocument(fileBytes, Convert.ToInt32(file.ContentLength));
    //        //DocxProcessingService.ParseUploadedDocument(ImageData, Convert.ToInt32(fs.Length));

    //        using (MemoryStream ms = new MemoryStream())
    //        {
    //            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
    //            {
    //                file.CopyTo(ms);
    //                // Open the package with read access.
    //                //var pippo = data.GetType();
    //                // Open a WordprocessingDocument for editing using the filepath.
    //                DocumentFormat.OpenXml.Packaging.WordprocessingDocument wordprocessingDocument = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Open(ms, true);

    //                // Assign a reference to the existing document body.
    //                DocumentFormat.OpenXml.Wordprocessing.Body body = wordprocessingDocument.MainDocumentPart.Document.Body;

    //                var paragraphs = body.ChildElements.Where(x => !string.IsNullOrEmpty(x.InnerText) ).Select(x => new DellaSanta.Core.ParsedParagraph { Id = x.InnerText.GetHashCode(), Text = x.InnerText, Repetitions_In_Document = 0 }).ToList();


    //                var luceneVersion = Lucene.Net.Util.Version.LUCENE_30;
                 
    //                using (RAMDirectory directory = new RAMDirectory())
    //                using (Analyzer analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(luceneVersion))
    //                {
    //                    using (IndexWriter ixw = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
    //                    {

    //                        foreach (var par in paragraphs)
    //                        {
    //                            Document document = new Document();
    //                            document.Add(new Lucene.Net.Documents.Field("Id", par.Text.GetHashCode().ToString(), Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.NOT_ANALYZED, Lucene.Net.Documents.Field.TermVector.NO));
    //                            document.Add(new Lucene.Net.Documents.Field("content", par.Text, Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
    //                            ixw.AddDocument(document);
    //                        }
    //                        ixw.Commit();

    //                        var parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "content", analyzer);
    //                        parser.DefaultOperator = QueryParser.Operator.AND;
    //                        Lucene.Net.Search.IndexSearcher indexSearcher = new IndexSearcher(directory, true);
                           
    //                        foreach (var paragraph in paragraphs)
    //                        {
    //                            StringBuilder qItem = new StringBuilder("content:");
    //                            qItem.Append(paragraph.Text.Replace(":", ""));
    //                            var query = parser.Parse(qItem.ToString());
    //                            TopDocs result = indexSearcher.Search(query, 20);
    //                            paragraph.Repetitions_In_Document = result.TotalHits;
                           
    //                        }
    //                    }
    //                }

    //                //TODO modify original doc

    //                // Locate the first paragraph in the document.
                 
    //                //XMLParagraphAlias firstParagraph = document.MainDocumentPart.Document.Descendants<XMLParagraphAlias>().First();

    //                //XMLCommentsAlias comments = document.MainDocumentPart.WordprocessingCommentsPart.Comments;

    //                //string id = comments.Descendants<DocumentFormat.OpenXml.Wordprocessing.Comment>()
    //                //     .Where(i => i.Id.Value == reply.CurrentCommentID.ToString())
    //                //     .Select(e => e.Id.Value)
    //                //     .First();


    //                //Modify original doc
    //                //var paragraphs = body.ChildElements.Select(x => new DellaSanta.Core.Paragraph { Id = x.InnerText.GetHashCode(), Text = x.InnerText, Repetitions_In_Document = 0 }).ToList();
    //                //foreach (var item in body.ChildElements)
    //                //{
    //                //    var par = paragraphs.Where(x => x.Text == item.InnerText).FirstOrDefault();
    //                //    if (null != par && par.Repetitions_In_Document > 1)
    //                //    {
    //                //        item.AddAnnotation("Repeated: " + par.Repetitions_In_Document);
    //                //    }
    //                //}
    //                //wordprocessingDocument.Save();

    //                //var success = true;



    //            }



    //        }

    //    }
    //}
}
