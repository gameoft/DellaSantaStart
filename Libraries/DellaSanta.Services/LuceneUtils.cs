using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DellaSanta.Core;

namespace DellaSanta.Services
{
    public class LuceneUtils
    {

        public static List<ParsedParagraph> SearchRepetitions(List<ParsedParagraph> paragraphs)
        {
            var luceneVersion = Lucene.Net.Util.LuceneVersion.LUCENE_48;

            using (var directory = new Lucene.Net.Store.RAMDirectory())
            using (Lucene.Net.Analysis.Analyzer analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(luceneVersion))
            {
             
                using (Lucene.Net.Index.IndexWriter ixw = new Lucene.Net.Index.IndexWriter(directory, new Lucene.Net.Index.IndexWriterConfig(luceneVersion, analyzer)))
                {

                    foreach (var par in paragraphs)
                    {
                        var document = new Lucene.Net.Documents.Document()
                                            {
                                                new Lucene.Net.Documents.TextField("content", par.HashedXml, Lucene.Net.Documents.Field.Store.YES)
                                            };
                        //document.Add(new Lucene.Net.Documents.Field("content", par.HashedText, Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                        ixw.AddDocument(document);
                    }
                    ixw.Commit();

                    var parser = new Lucene.Net.QueryParsers.Classic.QueryParser(luceneVersion, "content", analyzer);
                    parser.DefaultOperator = Lucene.Net.QueryParsers.Classic.QueryParser.AND_OPERATOR;
                    var searcherManager = new Lucene.Net.Search.SearcherManager(ixw, true, null);

                    var searcher = searcherManager.Acquire();
                    
                    foreach (var paragraph in paragraphs)
                    {
                        StringBuilder qItem = new StringBuilder("content:");
                        qItem.Append(paragraph.HashedXml);
                        var query = parser.Parse(qItem.ToString());
                        Lucene.Net.Search.TopDocs result = searcher.Search(query, 20);
                        paragraph.Repetitions_In_Document = result.TotalHits;

                    }
                }
            }

            return paragraphs;

        }

        #region 30
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

        #endregion 30
    }
}
