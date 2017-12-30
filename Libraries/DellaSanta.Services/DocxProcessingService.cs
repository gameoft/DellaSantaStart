using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DellaSanta.Core;


namespace DellaSanta.Services
{
    public class DocxProcessingService
    {

        public static bool Parse(string fileName)
        {
            try
            {
                List<ParsedParagraph> processedParagraphs = null;
                bool retValue = false;
                using (var wordprocessingDocument = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Open(fileName, true))
                {

                    processedParagraphs =
                        wordprocessingDocument.MainDocumentPart.Document.Descendants<DocumentFormat.OpenXml.Wordprocessing.Paragraph>()
                        .Where(p => !string.IsNullOrEmpty(p.InnerText))
                        .Select(x => new ParsedParagraph
                        {
                            Text = x.InnerText,
                            HashedXml = Hash(x.InnerXml),
                            Repetitions_In_Document = 0
                        })
                        .ToList();

                    LuceneUtils.SearchRepetitions(processedParagraphs);

                    retValue = AddComments(processedParagraphs, wordprocessingDocument);
                    return retValue;
            }
            }
            catch (Exception exc)
            {
                var msg = exc.Message;
                return false;
                //throw;
            }
        
        }

        private static string Hash(string input)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            var hashedinput = string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
            return hashedinput.Replace(':', '§');
        }

        private static bool AddComments(List<ParsedParagraph> processedParagraphs, DocumentFormat.OpenXml.Packaging.WordprocessingDocument wordprocessingDocument)
        {
          
                // Locate the first paragraph in the document.
                var listParagraphs =
                    wordprocessingDocument.MainDocumentPart.Document.Descendants<DocumentFormat.OpenXml.Wordprocessing.Paragraph>().ToList();

                for (int i = 0; i < listParagraphs.Count; i++)
                {
                    if (!string.IsNullOrEmpty(listParagraphs[i].InnerText))
                    {
                        var rep = processedParagraphs.Where(x => x.HashedXml == Hash(listParagraphs[i].InnerXml)).FirstOrDefault();
                        if (null != rep && rep.Repetitions_In_Document > 1)
                        {
                            DocumentFormat.OpenXml.Wordprocessing.Comments comments = null;
                            string id = "0";

                            if (wordprocessingDocument.MainDocumentPart.GetPartsCountOfType<DocumentFormat.OpenXml.Packaging.WordprocessingCommentsPart>() > 0)
                            {
                                comments =
                                    wordprocessingDocument.MainDocumentPart.WordprocessingCommentsPart.Comments;
                                if (comments.HasChildren)
                                {
                                    id = comments.Descendants<DocumentFormat.OpenXml.Wordprocessing.Comment>().Select(e => e.Id.Value).Max();
                                }
                            }
                            else
                            {
                                var commentPart =
                                            wordprocessingDocument.MainDocumentPart.AddNewPart<DocumentFormat.OpenXml.Packaging.WordprocessingCommentsPart>();
                                commentPart.Comments = new DocumentFormat.OpenXml.Wordprocessing.Comments();
                                comments = commentPart.Comments;
                            }

                            // Compose a new Comment and add it to the Comments part.
                            var p = new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text("Repetitions: " + rep.Repetitions_In_Document.ToString())));
                            var cmt = new DocumentFormat.OpenXml.Wordprocessing.Comment()
                            {
                                Id = id,
                                Author = "Automatic",
                                Date = DateTime.Now
                            };

                            cmt.AppendChild(p);
                            comments.AppendChild(cmt);
                            comments.Save();

                            // Specify the text range for the Comment. 
                            // Insert the new CommentRangeStart before the first run of paragraph.
                            listParagraphs[i].InsertBefore(new DocumentFormat.OpenXml.Wordprocessing.CommentRangeStart() { Id = id }, listParagraphs[i].GetFirstChild<DocumentFormat.OpenXml.Wordprocessing.Run>());

                            // Insert the new CommentRangeEnd after last run of paragraph.
                            var cmtEnd = listParagraphs[i].InsertAfter(new DocumentFormat.OpenXml.Wordprocessing.CommentRangeEnd() { Id = id }, listParagraphs[i].Elements<DocumentFormat.OpenXml.Wordprocessing.Run>().Last());

                            // Compose a run with CommentReference and insert it.
                            listParagraphs[i].InsertAfter(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.CommentReference() { Id = id }), cmtEnd);

                        }

                    }

                }

            return true;
        }


        #region Obsolete

        //public static List<DellaSanta.Core.ParsedParagraph> Parse(string fileName)
        //{
        //    List<DellaSanta.Core.ParsedParagraph> paragraphs = null;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        //        {
        //            file.CopyTo(ms);
        //            // Open the package with read access.
        //            // Open a WordprocessingDocument for editing using the filepath.
        //            DocumentFormat.OpenXml.Packaging.WordprocessingDocument wordprocessingDocument = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Open(ms, true);

        //            // Assign a reference to the existing document body.
        //            DocumentFormat.OpenXml.Wordprocessing.Body body = wordprocessingDocument.MainDocumentPart.Document.Body;

        //            paragraphs = body.ChildElements
        //                        .Where(x => !string.IsNullOrEmpty(x.InnerText))
        //                        .Select(x => new DellaSanta.Core.ParsedParagraph
        //                        {
        //                            Id = x.InnerText.GetHashCode(),
        //                            Text = x.InnerText,
        //                            Repetitions_In_Document = 0
        //                        })
        //                        .ToList();


        //            SearchRepetitions(paragraphs);

        //            // Close the handle explicitly.
        //            wordprocessingDocument.Close();

        //            return paragraphs;
        //        }
        //    }
        //}

        //public static List<DellaSanta.Core.ParsedParagraph> SearchRepetitions(List<DellaSanta.Core.ParsedParagraph> paragraphs)
        //{
        //    var luceneVersion = Lucene.Net.Util.Version.LUCENE_30;

        //    using (var directory = new Lucene.Net.Store.RAMDirectory())
        //    using (Lucene.Net.Analysis.Analyzer analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(luceneVersion))
        //    {
        //        using (Lucene.Net.Index.IndexWriter ixw = new Lucene.Net.Index.IndexWriter(directory, analyzer, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED))
        //        {

        //            foreach (var par in paragraphs)
        //            {
        //                var document = new Lucene.Net.Documents.Document();
        //                document.Add(new Lucene.Net.Documents.Field("Id", par.Text.GetHashCode().ToString(), Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.NOT_ANALYZED, Lucene.Net.Documents.Field.TermVector.NO));
        //                document.Add(new Lucene.Net.Documents.Field("content", par.Text, Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
        //                ixw.AddDocument(document);
        //            }
        //            ixw.Commit();

        //            var parser = new Lucene.Net.QueryParsers.QueryParser(Lucene.Net.Util.Version.LUCENE_30, "content", analyzer);
        //            parser.DefaultOperator = Lucene.Net.QueryParsers.QueryParser.Operator.AND;
        //            var indexSearcher = new Lucene.Net.Search.IndexSearcher(directory, true);

        //            foreach (var paragraph in paragraphs)
        //            {
        //                StringBuilder qItem = new StringBuilder("content:");
        //                qItem.Append(paragraph.Text.Replace(":", ""));
        //                var query = parser.Parse(qItem.ToString());
        //                Lucene.Net.Search.TopDocs result = indexSearcher.Search(query, 20);
        //                paragraph.Repetitions_In_Document = result.TotalHits;

        //            }
        //        }
        //    }

        //    return paragraphs;

        //}

        //public static bool AddComments(List<DellaSanta.Core.ParsedParagraph> reps, string fileName)
        //{
        //    // Open a WordprocessingDocument for editing using the filepath.
        //    using (var wordprocessingDocument = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Open(fileName, true))
        //    {

        //        // Locate the first paragraph in the document.
        //        var listParagraphs =
        //            wordprocessingDocument.MainDocumentPart.Document.Descendants<DocumentFormat.OpenXml.Wordprocessing.Paragraph>().ToList();

        //        for (int i = 0; i < listParagraphs.Count; i++)
        //        {
        //            if (!string.IsNullOrEmpty(listParagraphs[i].InnerText))
        //            {
        //                var rep = reps.Where(x => x.Text == listParagraphs[i].InnerText).FirstOrDefault();
        //                if (null != rep && rep.Repetitions_In_Document > 1)
        //                {
        //                    DocumentFormat.OpenXml.Wordprocessing.Comments comments = null;
        //                    string id = "0";

        //                    if (wordprocessingDocument.MainDocumentPart.GetPartsCountOfType<DocumentFormat.OpenXml.Packaging.WordprocessingCommentsPart>() > 0)
        //                    {
        //                        comments =
        //                            wordprocessingDocument.MainDocumentPart.WordprocessingCommentsPart.Comments;
        //                        if (comments.HasChildren)
        //                        {
        //                            id = comments.Descendants<DocumentFormat.OpenXml.Wordprocessing.Comment>().Select(e => e.Id.Value).Max();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        var commentPart =
        //                                    wordprocessingDocument.MainDocumentPart.AddNewPart<DocumentFormat.OpenXml.Packaging.WordprocessingCommentsPart>();
        //                        commentPart.Comments = new DocumentFormat.OpenXml.Wordprocessing.Comments();
        //                        comments = commentPart.Comments;
        //                    }

        //                    // Compose a new Comment and add it to the Comments part.
        //                    var p = new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text("Repetitions: " + rep.Repetitions_In_Document.ToString())));
        //                    var cmt = new DocumentFormat.OpenXml.Wordprocessing.Comment()
        //                    {
        //                        Id = id,
        //                        Author = "Automatic",
        //                        Date = DateTime.Now
        //                    };

        //                    cmt.AppendChild(p);
        //                    comments.AppendChild(cmt);
        //                    comments.Save();

        //                    // Specify the text range for the Comment. 
        //                    // Insert the new CommentRangeStart before the first run of paragraph.
        //                    listParagraphs[i].InsertBefore(new DocumentFormat.OpenXml.Wordprocessing.CommentRangeStart() { Id = id }, listParagraphs[i].GetFirstChild<DocumentFormat.OpenXml.Wordprocessing.Run>());

        //                    // Insert the new CommentRangeEnd after last run of paragraph.
        //                    var cmtEnd = listParagraphs[i].InsertAfter(new DocumentFormat.OpenXml.Wordprocessing.CommentRangeEnd() { Id = id }, listParagraphs[i].Elements<DocumentFormat.OpenXml.Wordprocessing.Run>().Last());

        //                    // Compose a run with CommentReference and insert it.
        //                    listParagraphs[i].InsertAfter(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.CommentReference() { Id = id }), cmtEnd);

        //                }

        //            }

        //        }




        //    }
        //    return true;
        //}


        #endregion Obsolete

    }
}
