using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMind.Acutance.Concepts
{
    using System.IO;

    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Acutance.Parsers.Grammars;

    using Sprache;

    public class KnowledgeLoader
    {
        public static KnowledgeFileQuery LoadFile(string path, int offset)
        {
            if (Path.GetExtension(path) != ".md")
            {
                return null;
            }

            var module = new KnowledgeFile(path);
            var query  = new KnowledgeFileQuery(module);

            var lineList = File.ReadLines(path) as IList<string> ?? File.ReadLines(path).ToList();
            for (var lineNum = offset; lineNum < lineList.Count; lineNum++)
            {
                var line = lineList[lineNum];

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var result = KnowledgeGrammar.TitleWithTimeTagParser.TryParse(line);
                if (result.WasSuccessful)
                {
                    var title     = result.Value;
                    var knowledge = new Knowledge(title, path, lineNum);
                    var entry     = new KnowledgeEntry(knowledge);

                    module.AddKnowledge(knowledge);
                    query .AddEntry(entry);
                }
                else
                {
                    var entry = new KnowledgeEntry(line, false);
                    query.AddEntry(entry);
                }
            }

            return query;
        }
    }
}
