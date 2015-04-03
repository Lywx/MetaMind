namespace MetaMind.Acutance.Concepts
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Acutance.Parsers.Grammars;

    using Sprache;

    public class KnowledgeLoader
    {
        public static KnowledgeQuery LoadFile(string path, int offset)
        {
            if (Path.GetExtension(path) != ".md")
            {
                return null;
            }

            var module = new KnowledgeFile(path);
            var buffer = new KnowledgeFileBuffer(module);
            var query  = new KnowledgeQuery(buffer);

            var lineList = File.ReadLines(path) as IList<string> ?? File.ReadLines(path).ToList();
            for (var lineNum = offset; lineNum < lineList.Count; lineNum++)
            {
                var line = lineList[lineNum];

                var withTimeTag    = KnowledgeGrammar.TitleWithTimeTagParser.TryParse(line);
                var withoutTimeTag = KnowledgeGrammar.TitleParser           .TryParse(line);

                if (withTimeTag.WasSuccessful)
                {
                    LoadTitleWithTimetag(path, withTimeTag.Value, lineNum, module, query, buffer);
                }
                else if (withoutTimeTag.WasSuccessful)
                {
                    LoadTitleWithoutTimetag(withoutTimeTag.Value, query, buffer);
                }
                else
                {
                    LoadLine(line, query);
                }
            }

            return query;
        }

        private static void LoadLine(string line, KnowledgeQuery query)
        {
            var entry = new KnowledgeEntry(line, false);
            query.AddEntry(entry);
        }

        private static void LoadTitleWithTimetag(string path, Title title, int lineNum, KnowledgeFile module, KnowledgeQuery query, KnowledgeFileBuffer buffer)
        {
            switch (title.Type)
            {
                case TitleType.Normal:
                    {
                        var knowledge = new Knowledge(title, path, lineNum);
                        var entry     = new KnowledgeEntry(knowledge);

                        // titles with time tag added to module and query
                        module.AddKnowledge(knowledge);
                        query .AddEntry(entry);
                    }

                    break;

                case TitleType.Link:
                    {
                        var link = new KnowledgeLink(title.Name);
                        buffer.AddLink(link);

                        // links normally won't contains time tag 
                        // so it won't be added to module but query
                        var entry = new KnowledgeEntry(title);
                        query.AddEntry(entry);
                    }

                    break;
            }
        }

        private static void LoadTitleWithoutTimetag(Title title, KnowledgeQuery query, KnowledgeFileBuffer buffer)
        {
            switch (title.Type)
            {
                case TitleType.Normal:
                    {
                        var entry = new KnowledgeEntry(title);

                        // titles without time tag
                        // won't add to module but will be added to query
                        query.AddEntry(entry);
                    }

                    break;

                case TitleType.Link:
                    {
                        var link = new KnowledgeLink(title.Name);
                        buffer.AddLink(link);

                        // titles without time tag
                        // won't add to module but will be added to query
                        var entry = new KnowledgeEntry(title);
                        query.AddEntry(entry);
                    }

                    break;
            }
        }
    }
}
