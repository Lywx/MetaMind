#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace MetaMind.Next
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameEngine()) game.Run();
        }
    }
#endif
}
