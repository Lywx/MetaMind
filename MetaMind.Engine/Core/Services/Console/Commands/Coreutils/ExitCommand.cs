// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExitCommand.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Core.Services.Console.Commands.Coreutils
{
    using Entity.Common;

    internal class ExitCommand : MMEntity, IConsoleCommand
    {
        public ExitCommand()
        {
        }

        public string Name => "exit";

        public string Description => "Exists the engine";

        public string Execute(string[] arguments)
        {
            this.GlobalInterop.Engine.Exit();
            
            return "Exiting the engine";
        }
    }
}