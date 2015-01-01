using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Parsers;

    public static class CommandParser
    {
        public static CommandEntry ParseLine(string line, string path)
        {
            Parser.ParseLine(line, "[(.*)]")
            new CommandEntry(name, path, )
        }
    }
}
