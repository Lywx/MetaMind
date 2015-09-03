namespace MetaMind.Engine.Console
{
    using System.Collections.Generic;
    using Commands;
    using Components.Fonts;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class GameConsoleSettings
    {
        #region Behavior

        public bool OpenOnWrite { get; set; } = true;

        #endregion

        #region Colors

        public Color BackgroundColor { get; set; } = new Color(0, 0, 0, 125);

        public Color FontColor
        {
            set
            {
                this.PastBufferColor =
                    this.PastOutputColor =
                    this.PastDebugColor =
                    this.PastErrorColor =
                    this.BufferColor =
                    this.PromptColor = this.CursorColor = value;
            }
        }

        public Color BufferColor { get; set; }

        public Color PastBufferColor { get; set; }

        public Color PastOutputColor { get; set; }

        public Color PastDebugColor { get; set; }

        public Color PastErrorColor { get; set; }

        public Color PromptColor { get; set; }

        public Color CursorColor { get; set; }

        #endregion

        #region Keys

        public Keys ToggleKey { get; set; } = Keys.OemTilde;

        #endregion

        #region Graphics

        public float AnimationSpeed { get; set; } = 1;

        public float CursorBlinkSpeed { get; set; } = 0.5f;

        public int Height { get; set; } = 300;

        /// <summary>
        /// Prompt symbol for the console.
        /// </summary>
        public string Prompt { get; set; } = "$";

        /// <summary>
        /// Cusor symbol for the console.
        /// </summary>
        public char Cursor { get; set; } = '_';

        /// <summary>
        /// Four-sided padding for string inside console bounds.
        /// </summary>
        public int Padding { get; set; } = 30;

        /// <summary>
        /// Horizontal margin for console display.
        /// </summary>
        public int Margin { get; set; } = 30;

        public Font Font { get; set; }

        public Texture2D RoundedCorner { get; set; }

        #endregion

        #region Commands

        public List<IConsoleCommand> Commands { get; set; } = new List<IConsoleCommand>();

        #endregion

        #region Constructors

        public GameConsoleSettings()
        {
            this.FontColor = Color.White; 
        }

        #endregion
    }
}