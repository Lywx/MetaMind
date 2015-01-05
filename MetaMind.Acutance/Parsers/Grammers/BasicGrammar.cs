// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicGrammar.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Parsers.Grammers
{
    using System.Linq;

    using MetaMind.Acutance.Parsers.Elements;

    using Sprache;

    public static class BasicGrammar
    {
        public static Parser<string> BracketedTextParser = (from lquot in Parse.Char('[')
                                                            from content in Parse.CharExcept(']').Many().Text()
                                                            from rquot in Parse.Char(']')
                                                            select content).Token();

        public static Parser<string> WordParser = Parse.Letter.AtLeastOnce().Text().Token();

        public static Parser<Sentence> SentenceParser = from words in WordParser.AtLeastOnce()
                                                        select new Sentence(words.ToList());
    }
}