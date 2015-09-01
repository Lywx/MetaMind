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
    using Commands;
    using Components.Fonts;
    using Microsoft.Xna.Framework.Graphics;

    public class GameConsole : IDisposable
    {
        private readonly GameEngine engine;

        private readonly GameConsoleComponent console;

        #region Constructors and Destructor

        public GameConsole(GameEngine engine, SpriteBatch spriteBatch, IStringDrawer stringDrawer)
            : this(new GameConsoleSettings(), engine, new IConsoleCommand[0], new CommandProcessor(), spriteBatch, stringDrawer)
        {
        }

        public GameConsole(GameEngine engine, SpriteBatch spriteBatch, IStringDrawer stringDrawer, IEnumerable<IConsoleCommand> commands)
            : this(new GameConsoleSettings(), engine, commands, new CommandProcessor(), spriteBatch, stringDrawer)
        {
        }

        public GameConsole(GameConsoleSettings settings, GameEngine engine, SpriteBatch spriteBatch, IStringDrawer stringDrawer)
            : this(settings, engine, new IConsoleCommand[0], new CommandProcessor(), spriteBatch, stringDrawer)
        {
        }

        public GameConsole(GameConsoleSettings settings, GameEngine engine, IEnumerable<IConsoleCommand> commands, CommandProcessor commandProcessor, SpriteBatch spriteBatch, IStringDrawer stringDrawer)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.engine = engine;

            // Has to initialized before GameConsoleComponent
            GameConsoleSettings.Settings = settings;
            GameConsoleSettings.Commands = commands.ToList();

            this.console = new GameConsoleComponent(engine, this, new GameConsoleProcessor(engine, commandProcessor), spriteBatch, stringDrawer);

            this.engine.Components.Add(this.console);

            this.Enabled = true;
        }

        #endregion

        public bool Enabled { get; set; }

        /// <summary>
        ///     Indicates whether the console is currently opened
        /// </summary>
        public bool Opened => this.console.IsOpen;

        public List<IConsoleCommand> Commands => GameConsoleSettings.Commands;

        public ICommandProcessor CommandProcessor
        {
            get { return this.console.Processer.CommandProcessor; }
            set { this.console.Processer.CommandProcessor = value; }
        }

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
                this.console.WriteLine(buffer, OutputType.Debug);
            }
            else if (string.Compare(channel, "ERROR", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.console.WriteLine(buffer, OutputType.Error);
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            this.console.Dispose();
        }

        #endregion
    }
}