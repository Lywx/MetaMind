namespace MetaMind.Engine.Core.Services.Console.Commands
{
    using System;
    using System.Collections.Generic;

    internal class CommandComparer : IComparer<IConsoleCommand>
    {
        public int Compare(IConsoleCommand x, IConsoleCommand y)
        {
            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }
}
