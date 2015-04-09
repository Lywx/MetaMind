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

            var module = new RawKnowledgeFile(path);
            var buffer = new RawKnowledgeFileBuffer(module);
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
            var entry = new Knowledge(line, false);
            query.Add(entry);
        }

        private static void LoadTitleWithTimetag(string path, Title title, int lineNum, RawKnowledgeFile module, KnowledgeQuery query, RawKnowledgeFileBuffer buffer)
        {
            switch (title.Type)
            {
                case TitleType.Normal:
                    {
                        var knowledge = new RawKnowledge(title, path, lineNum);
                        var entry     = new Knowledge(knowledge);

                        // titles with time tag added to module and query
                        module.AddKnowledge(knowledge);
                        query .Add(entry);
                    }

                    break;

                case TitleType.Link:
                    {
                        var link = new RawKnowledgeLink(title.Name);
                        buffer.AddLink(link);

                        // links normally won't contains time tag 
                        // so it won't be added to module but query
                        var entry = new Knowledge(title);
                        query.Add(entry);
                    }

                    break;
            }
        }

        private static void LoadTitleWithoutTimetag(Title title, KnowledgeQuery query, RawKnowledgeFileBuffer buffer)
        {
            switch (title.Type)
            {
                case TitleType.Normal:
                    {
                        var entry = new Knowledge(title);

                        // titles without time tag
                        // won't add to module but will be added to query
                        query.Add(entry);
                    }

                    break;

                case TitleType.Link:
                    {
                        var link = new RawKnowledgeLink(title.Name);
                        buffer.AddLink(link);

                        // titles without time tag
                        // won't add to module but will be added to query
                        var entry = new Knowledge(title);
                        query.Add(entry);
                    }

                    break;
            }
        }
    }
}
