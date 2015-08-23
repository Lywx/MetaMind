#region File Description

//-----------------------------------------------------------------------------
// MonoGame Console https://github.com/romanov/MonoGameConsole
// Forked from http://code.google.com/p/xnagameconsole/
// GNU GPL v3
//-----------------------------------------------------------------------------

#endregion File Description

namespace MetaMind.Engine.Guis.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Components.Fonts;
    using Commands;

    using Microsoft.Xna.Framework.Graphics;

    public class GameConsole
    {
        private readonly GameConsoleComponent console;

        public GameConsole(GameEngine engine, SpriteBatch spriteBatch, IStringDrawer stringDrawer)
            : this(engine, spriteBatch, stringDrawer, new IConsoleCommand[0], new GameConsoleSettings())
        {
        }

        public GameConsole(GameEngine engine, SpriteBatch spriteBatch, IStringDrawer stringDrawer, GameConsoleSettings settings)
            : this(engine, spriteBatch, stringDrawer, new IConsoleCommand[0], settings)
        {
        }

        public GameConsole(GameEngine engine, SpriteBatch spriteBatch, IStringDrawer stringDrawer, IEnumerable<IConsoleCommand> commands)
            : this(engine, spriteBatch, stringDrawer, commands, new GameConsoleSettings())
        {
        }

        public GameConsole(GameEngine engine, SpriteBatch spriteBatch, IStringDrawer stringDrawer, IEnumerable<IConsoleCommand> commands, GameConsoleSettings settings)
        {
            // Has to initialized before GameConsoleComponent
            GameConsoleSettings.Settings  = settings;
            GameConsoleSettings.Commands = commands.ToList();
            
            this.console = new GameConsoleComponent(engine, this, spriteBatch, stringDrawer);
            this.Enabled = true;

            engine.Components.Add(this.console);
        }

        public List<IConsoleCommand> Commands => GameConsoleSettings.Commands;

        public bool Enabled { get; set; }

        /// <summary>
        ///     Indicates whether the console is currently opened
        /// </summary>
        public bool Opened => this.console.IsOpen;

        #region Operations

        /// <summary>
        /// Adds a new command to the console
        /// </summary>
        /// <param name="commands"></param>
        public void AddCommand(params IConsoleCommand[] commands)
        {
            this.Commands.AddRange(commands);
        }

        /// <summary>
        ///     Adds a new command to the console
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="action"></param>
        public void AddCommand(string name, Func<string[], string> action)
        {
            this.AddCommand(name, action, string.Empty);
        }

        /// <summary>
        ///     Adds a new command to the console
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        public void AddCommand(string name, Func<string[], string> action, string description)
        {
            this.Commands.Add(new CustomCommand(name, description, action));
        }

        /// <summary>
        ///     Write directly to the output stream of the console
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="channel">Channel to write to, include "", "DEBUG", "ERROR"</param>
        public void WriteLine(string buffer, string channel = "")
        {
            if (string.IsNullOrEmpty(channel))
            {
                this.console.WriteLine(buffer);
            }
            else if (string.Compare(channel, "DEBUG", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.console.WriteLine(buffer, OutputLineType.Debug);
            }
            else if (string.Compare(channel, "ERROR", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.console.WriteLine(buffer, OutputLineType.Error);
            }
        }

        #endregion
    }
}