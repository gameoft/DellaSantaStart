using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Index.Memory;
using Lucene.Net.Store;

namespace DellaSanta.Services
{
    public class DocxProcessingService
    {
        public static object PatternAnalyzer { get; private set; }

        public static bool ProcessDocument(string FileName)
        {

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (FileStream file = new FileStream(FileName, FileMode.Open, FileAccess.Read))
                    {
                        file.CopyTo(ms);
                        // Open the package with read access.
                        // Open a WordprocessingDocument for editing using the filepath.
                        DocumentFormat.OpenXml.Packaging.WordprocessingDocument wordprocessingDocument = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Open(ms, true);

                        // Assign a reference to the existing document body.
                        DocumentFormat.OpenXml.Wordprocessing.Body body = wordprocessingDocument.MainDocumentPart.Document.Body;

                        var paragraphs = body.ChildElements.Where(x => !string.IsNullOrEmpty(x.InnerText)).Select(x => new DellaSanta.Core.Paragraph { Id = x.InnerText.GetHashCode(), Text = x.InnerText, Repetitions_In_Document = 0 }).ToList();

                        //Find repetitions
                        var luceneVersion = Lucene.Net.Util.Version.LUCENE_30;
                        using (RAMDirectory directory = new RAMDirectory())
                        using (Analyzer analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(luceneVersion))
                        {
                            using (IndexWriter ixw = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
                            {

                                foreach (var par in paragraphs)
                                {
                                    Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();
                                    document.Add(new Field("Id", par.Text.GetHashCode().ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                                    document.Add(new Field("content", par.Text, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                                    ixw.AddDocument(document);
                                }
                                ixw.Commit();

                                var parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "content", analyzer);
                                parser.DefaultOperator = QueryParser.Operator.AND;
                                Lucene.Net.Search.IndexSearcher indexSearcher = new IndexSearcher(directory, true);
                                //int totRepeatedParagraphs = 0;
                                foreach (var paragraph in paragraphs)
                                {
                                    StringBuilder qItem = new StringBuilder("content:");
                                    qItem.Append(paragraph.Text);
                                    var query = parser.Parse(qItem.ToString());
                                    TopDocs result = indexSearcher.Search(query, 20);
                                    paragraph.Repetitions_In_Document = result.TotalHits;
                                    //if (result.TotalHits > 1)
                                    //    totRepeatedParagraphs++;
                                }
                            }
                        }

                        //TODO TODO TODO TODO TODO TODO               //Modify original doc

                        //var paragraphs = body.ChildElements.Select(x => new DellaSanta.Core.Paragraph { Id = x.InnerText.GetHashCode(), Text = x.InnerText, Repetitions_In_Document = 0 }).ToList();
                        //foreach (var item in body.ChildElements)
                        //{
                        //    var par = paragraphs.Where(x => x.Text == item.InnerText).FirstOrDefault();
                        //    if (null != par)
                        //    {
                        //        item.AddAnnotation("Repeated: " + par.Repetitions_In_Document);
                        //    }
                        //}
                        //wordprocessingDocument.Save();
                        return true;
                    }

                }
                
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                //throw;
                return false;
            }

           



            //try
            //{
            //    using (MemoryStream upFile = new MemoryStream())
            //    {
            //        upFile.Write(FileBytes, 0, ContentLength);


            //        // Open the package with read access.
            //        //var pippo = data.GetType();
            //        // Open a WordprocessingDocument for editing using the filepath.
            //        WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(upFile, true);

            //        // Assign a reference to the existing document body.
            //        Body body = wordprocessingDocument.MainDocumentPart.Document.Body;

            //        var paragraphs = body.ChildElements.ToList();
            //        return paragraphs;

            //    }
            //}
            //catch (Exception ex)
            //{
            //    var msg = ex.Message;
            //    throw;
            //}

        }
    }
}
