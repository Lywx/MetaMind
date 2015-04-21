// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExitCommand.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoGameConsole.Commands
{
    using Microsoft.Xna.Framework;

    internal class ExitCommand : IConsoleCommand
    {
        private readonly Game game;

        public ExitCommand(Game game)
        {
            this.game = game;
        }

        public string Name
        {
            get
            {
                return "exit";
            }
        }

        public string Description
        {
            get
            {
                return "Forcefully exists the game";
            }
        }

        public string Execute(string[] arguments)
        {
            this.game.Exit();
            return "Exiting the game";
        }
    }
}