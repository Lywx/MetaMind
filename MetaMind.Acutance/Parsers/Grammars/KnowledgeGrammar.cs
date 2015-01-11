// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KnowledgeGrammar.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Parsers.Grammars
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Engine.Parsers.Grammars;

    using Sprache;

    public class KnowledgeGrammar
    {
        public static Parser<TimeTag> TimeTagFullParser = 
            from hours   in Parse.CharExcept(':').Many().Text()
            from lcolon  in Parse.Char(':')
            from minutes in Parse.CharExcept(':').Many().Text() 
            from rcolon  in Parse.Char(':') 
            from seconds in Parse.CharExcept(':').Many().Text()
            select new TimeTag(hours, minutes, seconds);

        public static Parser<TimeTag> TimeTagConciseParser = 
            from minutes in Parse.CharExcept(':').Many().Text() 
            from rcolon  in Parse.Char(':') 
            from seconds in Parse.CharExcept(':').Many().Text()
            select new TimeTag("00", minutes, seconds);

        public static Parser<Parser<TimeTag>> TimeTagStrategyParser =
            Parse.Regex(@"(\d+:\d+:\d+)", "Hours:Minutes:Seconds").Return(TimeTagFullParser)
                .Or(Parse.Regex(@"(\d+:\d+)", "Minutes:Seconds").Return(TimeTagConciseParser));

        public static Parser<TitleLevel> TitleLevelParser =
            Parse        .String("######").Return(TitleLevel.Six)
                .Or(Parse.String("#####") .Return(TitleLevel.Five))
                .Or(Parse.String("####")  .Return(TitleLevel.Four))
                .Or(Parse.String("###")   .Return(TitleLevel.Three))
                .Or(Parse.String("##")    .Return(TitleLevel.Two))
                .Or(Parse.String("#")     .Return(TitleLevel.One));

        public static Parser<IOption<char>> RepeativityParser = Parse.Char('!').Optional();

        public static Parser<Title> TitleParser = from level    in TitleLevelParser
                                                  from sentence in BasicGrammar.SentenceParser
                                                  from repeat   in RepeativityParser
                                                  select new Title(level, repeat.IsDefined ? RepeativityTag.EveryMoment : RepeativityTag.Unspecified, sentence);

        public static Parser<Title> TitleWithBracketParser = from level    in TitleLevelParser
                                                             from repeat   in RepeativityParser
                                                             from sentence in BasicGrammar.SentenceParser
                                                             from text     in BasicGrammar.BracketedTextParser
                                                             select new Title(level, repeat.IsDefined ? RepeativityTag.EveryMoment : RepeativityTag.Unspecified, sentence);

        public static Parser<Title> TitleWithTimeTagParser = from level    in TitleLevelParser
                                                             from repeat   in RepeativityParser
                                                             from sentence in BasicGrammar.SentenceParser
                                                             from text     in BasicGrammar.BracketedTextParser
                                                             select new Title(level, repeat.IsDefined ? RepeativityTag.EveryMoment : RepeativityTag.Unspecified,sentence, TimeTagStrategyParser.Parse(text).Parse(text));
    }
}