// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KnowledgeGrammar.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Parsers.Grammers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Parsers.Elements;

    using Sprache;

    public class KnowledgeGrammar
    {
        public static Parser<TitleLevel> TitleLevelParser =
            Parse        .String("######").Return(TitleLevel.Six)
                .Or(Parse.String("#####") .Return(TitleLevel.Five))
                .Or(Parse.String("####")  .Return(TitleLevel.Four))
                .Or(Parse.String("###")   .Return(TitleLevel.Three))
                .Or(Parse.String("##")    .Return(TitleLevel.Two))
                .Or(Parse.String("#")     .Return(TitleLevel.One));

        public static Parser<Title> TitleParser = from level    in TitleLevelParser
                                                  from sentence in BasicGrammar.SentenceParser
                                                  select new Title(level, sentence);

        public static Parser<Title> TitleWithBracketParser = from level    in TitleLevelParser
                                                             from sentence in BasicGrammar.SentenceParser
                                                             from text     in BasicGrammar.BracketedTextParser
                                                             select new Title(level, sentence);

        public static Parser<Title> TitleWithTimeTagParser = from level    in TitleLevelParser
                                                             from sentence in BasicGrammar.SentenceParser
                                                             from text     in BasicGrammar.BracketedTextParser
                                                             select new Title(level, sentence, TimeTagGrammer.TimeTagStrategyParser.Parse(text).Parse(text));

        public static KnowledgeModuleQuery LoadKnowledgeModule(string path)
        {
            if (Path.GetExtension(path) != ".md")
            {
                return null;
            }

            var module = new KnowledgeModule(path);
            var query  = new KnowledgeModuleQuery(module);

            var lineList = File.ReadLines(path) as IList<string> ?? File.ReadLines(path).ToList();
            for (var lineNum = 0; lineNum < lineList.Count; lineNum++)
            {
                var line = lineList[lineNum];

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var result = TitleWithTimeTagParser.TryParse(line);
                if (result.WasSuccessful)
                {
                    var title     = result.Value;
                    var knowledge = new Knowledge(title, module, lineNum);
                    var entry     = new KnowledgeEntry(knowledge);
                    query.AddEntry(entry);
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