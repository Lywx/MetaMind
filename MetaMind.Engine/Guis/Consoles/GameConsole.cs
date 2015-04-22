#region File Description

//-----------------------------------------------------------------------------
// MonoGame Console https://github.com/romanov/MonoGameConsole
// Forked from http://code.google.com/p/xnagameconsole/
// GNU GPL v3
//-----------------------------------------------------------------------------

#endregion File Description

namespace MetaMind.Engine.Guis.Consoles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Consoles.Commands;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GameConsole
    {
        public GameConsoleOptions Options
        {
            get
            {
                return GameConsoleOptions.Options;
            }
        }

        public List<IConsoleCommand> Commands
        {
            get
            {
                return GameConsoleOptions.Commands;
            }
        }

        public bool Enabled { get; set; }

        /// <summary>
        ///     Indicates whether the console is currently opened
        /// </summary>
        public bool Opened
        {
            get
            {
                return this.console.IsOpen;
            }
        }

        private readonly GameConsoleComponent console;

        public GameConsole(Game game, SpriteBatch spriteBatch, IStringDrawer stringDrawer)
            : this(game, spriteBatch, stringDrawer, new IConsoleCommand[0], new GameConsoleOptions())
        {
        }

        public GameConsole(Game game, SpriteBatch spriteBatch, IStringDrawer stringDrawer, GameConsoleOptions options)
            : this(game, spriteBatch, stringDrawer, new IConsoleCommand[0], options)
        {
        }

        public GameConsole(Game game, SpriteBatch spriteBatch, IStringDrawer stringDrawer, IEnumerable<IConsoleCommand> commands)
            : this(game, spriteBatch, stringDrawer, commands, new GameConsoleOptions())
        {
        }

        public GameConsole(Game game, SpriteBatch spriteBatch, IStringDrawer stringDrawer, IEnumerable<IConsoleCommand> commands, GameConsoleOptions options)
        {
            GameConsoleOptions.Options = options;
            GameConsoleOptions.Commands = commands.ToList();
            
            this.Enabled = true;
            this.console = new GameConsoleComponent(this, game, spriteBatch, stringDrawer);
            
            game.Services.AddService(typeof(GameConsole), this);
            game.Components.Add(this.console);
        }

        /// <summary>
        ///     Write directly to the output stream of the console
        /// </summary>
        /// <param name="text"></param>
        public void WriteLine(string text)
        {
            this.console.WriteLine(text);
        }

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
            this.AddCommand(name, action, "");
        }

        /// <summary>
        ///     Adds a new command to the console
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        public void AddCommand(string name, Func<string[], string> action, string description)
        {
            this.Commands.Add(new CustomCommand(name, action, description));
        }
    }
}