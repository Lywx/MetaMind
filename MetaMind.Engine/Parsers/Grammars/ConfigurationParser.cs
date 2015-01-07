using System.Collections.Generic;

namespace MetaMind.Engine.Parsers.Grammars
{
    using Sprache;

    public static class ConfigurationParser
    {
        public static Parser<KeyValuePair<string, string>> ConfigurationPairParser =

            // allow multiple word on left side
            from lhs in Parse.AnyChar.Except(Parse.Chars('=', '\n')).AtLeastOnce()
            from lspaces in Parse.WhiteSpace.Optional() 
            from eq in Parse.Char('=')
            from rspaces in Parse.WhiteSpace.Optional()

            // allow multiple word on right side
            from rhs in Parse.AnyChar.Except(Parse.Chars('=', '\n')).AtLeastOnce()
            select new KeyValuePair<string, string>(string.Concat(lhs).Trim(), string.Concat(rhs).Trim());

        public static Parser<KeyValuePair<string, string>> ConfigurationLineParser =

            // comment 
            Parse.Regex("(.)*(\")+").AtLeastOnce().Return(new KeyValuePair<string, string>())

                // pair
                .Or(from pair in ConfigurationPairParser select pair);
    }
}
