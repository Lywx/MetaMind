namespace MetaMind.Engine.Core.Services.Parser.Grammar
{
    using System.Linq;
    using Elements;
    using Sprache;

    public static class CommonGrammar
    {
        public static Parser<string> BracketParser =
            (from lquot in Parse.Char('[')
             from content in Parse.CharExcept(']').Many().Text()
             from rquot in Parse.Char(']')
             select content).Token();

        public static Parser<string> WordParser =
            Parse.AnyChar.Except(Parse.Chars(' ', '[')).
                  AtLeastOnce().
                  Text().
                  Token();

        public static Parser<Sentence> SentenceParser =
            from words in WordParser.AtLeastOnce()
            select new Sentence(words.ToList());
    }
}
