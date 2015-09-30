namespace MetaMind.Engine.Service.Parser.Grammar
{
    using System.Collections.Generic;
    using Sprache;

    public static class ConfigurationFileGrammar
    {
        public static Parser<KeyValuePair<string, string>> ConfigurationPairParser =

            // allow multiple word on left side
            from lhs in Parse.AnyChar.Except(Parse.Chars('=', '\"', '\n')).AtLeastOnce()
            from lspaces in Parse.WhiteSpace.Optional()
            from eq in Parse.Char('=')
            from rspaces in Parse.WhiteSpace.Optional()

                // allow multiple word on right side
            from rhs in Parse.AnyChar.Except(Parse.Chars('=', '\"', '\n')).AtLeastOnce()
            select new KeyValuePair<string, string>(string.Concat(lhs).Trim(), string.Concat(rhs).Trim());

        public static Parser<string> ConfigurationCommentParser = Parse.Regex("\"+(.*)");

        public static Parser<KeyValuePair<string, string>> ConfigurationLineParser =
            from pair in ConfigurationPairParser.Optional()
            from comment in ConfigurationCommentParser.Optional()
            select pair.IsDefined ? pair.Get() : new KeyValuePair<string, string>();
    }
}
