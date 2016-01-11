// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExitCommand.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services.Console.Commands.Coreutils
{
    using Entities;
    using Entities.Bases;

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