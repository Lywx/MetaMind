namespace MetaMind.Engine.Guis.Console
{
    using System;
    using System.Collections.Generic;
    using Commands;

    internal class CommandComparer : IComparer<IConsoleCommand>
    {
        public int Compare(IConsoleCommand x, IConsoleCommand y)
        {
            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }
}
