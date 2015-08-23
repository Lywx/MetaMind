namespace MetaMind.Engine.Guis.Console
{
    using System.Collections.Generic;

    using Commands;
    using Components.Fonts;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GameConsoleSettings
    {
        public int ToggleKey { get; set; }

        public Color BackgroundColor { get; set; }

        public Color FontColor
        {
            set
            {
                this.BufferColor = this.PastBufferColor = this.PastOutputColor = this.PastDebugColor = this.PastErrorColor = this.PromptColor = this.CursorColor = value;
            }
        }

        public Color BufferColor { get; set; }

        public Color PastBufferColor { get; set; }

        public Color PastOutputColor { get; set; }

        public Color PastDebugColor { get; set; }

        public Color PastErrorColor { get; set; }

        public Color PromptColor { get; set; }

        public Color CursorColor { get; set; }

        public float AnimationSpeed { get; set; }

        public float CursorBlinkSpeed { get; set; }

        public int Height { get; set; }

        /// <summary>
        /// Prompt symbol for the console.
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// Cusor symbol for the console.
        /// </summary>
        public char Cursor { get; set; }

        /// <summary>
        /// Four-sided padding for string inside console bounds.
        /// </summary>
        public int Padding { get; set; }

        /// <summary>
        /// Horizontal margin for console display.
        /// </summary>
        public int Margin { get; set; }

        public Font Font { get; set; }

        public bool OpenOnWrite { get; set; }

        public Texture2D RoundedCorner { get; set; }

        internal static GameConsoleSettings Settings { get; set; }

        internal static List<IConsoleCommand> Commands { get; set; }

        public GameConsoleSettings()
        {
            // Tilde code 192
            this.ToggleKey = 192; 

            this.BackgroundColor = new Color(0, 0, 0, 125);
            this.FontColor       = Color.White;

            this.AnimationSpeed   = 1;
            this.CursorBlinkSpeed = 0.5f;

            this.Height  = 300;
            this.Margin  = 30;
            this.Padding = 30;

            this.Prompt = "$";
            this.Cursor = '_';

            this.OpenOnWrite = true;
        }
    }
}