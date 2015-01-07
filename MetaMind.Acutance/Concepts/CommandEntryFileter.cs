using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMind.Acutance.Concepts
{
    public static class CommandEntryFileter
    {
        public static List<CommandEntry> FilterOutKnowledge(IEnumerable<CommandEntry> fromFile)
        {
            return fromFile.Where(commmand => commmand.Type == CommandType.Knowledge).ToList();
        }
    }
}
