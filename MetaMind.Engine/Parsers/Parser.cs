using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMind.Engine.Parsers
{
    using System.Text.RegularExpressions;

    public static class Parser
    {
        public static string ParseLine(string line, string pattern)
        {
            var matchEvent = Regex.Match(line, pattern);

            return matchEvent.Success ? matchEvent.Value : string.Empty;
        }
    }
}
