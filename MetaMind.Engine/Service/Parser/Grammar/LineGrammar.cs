// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LineGrammar.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Service.Parser.Grammar
{
    using System.Linq;
    using Elements;
    using Sprache;

    public static class LineGrammar
    {
        public static Parser<string> BracketedTextParser = (from lquot in Parse.Char('[')
                                                            from content in Parse.CharExcept(']').Many().Text()
                                                            from rquot in Parse.Char(']')
                                                            select content).Token();

        public static Parser<string> WordParser = Parse.AnyChar.Except(Parse.Chars(' ', '[')).AtLeastOnce().Text().Token();

        public static Parser<Sentence> SentenceParser = from words in WordParser.AtLeastOnce()
                                                        select new Sentence(words.ToList());
    }
}