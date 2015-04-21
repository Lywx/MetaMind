namespace MonoGameConsole.Commands
{
    using System.Collections.Generic;

    internal class CommandComparer : IComparer<IConsoleCommand>
    {
        public int Compare(IConsoleCommand x, IConsoleCommand y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
