using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoGameConsole
{
    using MetaMind.Engine.Components.Fonts;

    public class GameConsoleOptions
    {
        public int ToggleKey { get; set; }

        public Color BackgroundColor { get; set; }

        public Color FontColor
        {
            set
            {
                this.BufferColor = this.PastCommandColor = this.PastCommandOutputColor = this.PromptColor = this.CursorColor = value;
            }
        }

        public Color BufferColor { get; set; }

        public Color PastCommandColor { get; set; }

        public Color PastCommandOutputColor { get; set; }

        public Color PromptColor { get; set; }

        public Color CursorColor { get; set; }

        public float AnimationSpeed { get; set; }

        public float CursorBlinkSpeed { get; set; }

        public int Height { get; set; }

        public string Prompt { get; set; }

        public char Cursor { get; set; }

        public int Padding { get; set; }

        public int Margin { get; set; }

        public bool OpenOnWrite { get; set; }

        public Font Font { get; set; }

        public Texture2D RoundedCorner { get; set; }

        internal static GameConsoleOptions Options { get; set; }

        internal static List<IConsoleCommand> Commands { get; set; }

        public GameConsoleOptions()
        {
            // Default options
            this.ToggleKey = 192; // tilde
            this.BackgroundColor = new Color(0, 0, 0, 125);
            this.FontColor = Color.White;
            this.AnimationSpeed = 1;
            this.CursorBlinkSpeed = 0.5f;
            this.Height = 300;
            this.Prompt = "$";
            this.Cursor = '_';
            this.Padding = 30;
            this.Margin = 30;
            this.OpenOnWrite = true;
        }
    }
}