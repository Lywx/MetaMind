﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExitCommand.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Console.Commands
{
    internal class ExitCommand : IConsoleCommand
    {
        private readonly GameEngine engine;

        public ExitCommand(GameEngine engine)
        {
            this.engine = engine;
        }

        public string Name => "GameEngine.exit";

        public string Description => "Exists the engine";

        public string Execute(string[] arguments)
        {
            this.engine.Exit();
            
            return "Exiting the engine";
        }
    }
}