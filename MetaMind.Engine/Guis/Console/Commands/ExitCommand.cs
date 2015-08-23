// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExitCommand.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Console.Commands
{
    using Microsoft.Xna.Framework;

    internal class ExitCommand : IConsoleCommand
    {
        private readonly Game game;

        public ExitCommand(Game game)
        {
            this.game = game;
        }

        public string Name => "exit";

        public string Description => "Exists the engine";

        public string Execute(string[] arguments)
        {
            this.game.Exit();
            
            return "Exiting the engine";
        }
    }
}