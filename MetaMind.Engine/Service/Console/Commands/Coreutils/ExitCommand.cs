// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExitCommand.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Service.Console.Commands.Coreutils
{
    using Entities;

    internal class ExitCommand : MMEntity, IConsoleCommand
    {
        public ExitCommand()
        {
        }

        public string Name => "exit";

        public string Description => "Exists the engine";

        public string Execute(string[] arguments)
        {
            this.Interop.Engine.Exit();
            
            return "Exiting the engine";
        }
    }
}