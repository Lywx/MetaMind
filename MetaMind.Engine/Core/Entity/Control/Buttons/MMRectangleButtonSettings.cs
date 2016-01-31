namespace MetaMind.Engine.Core.Entity.Control.Buttons
{
    using Backend.Content.Fonts;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Settings;

    /// <summary>
    /// Rectangle button settings that could be consumed and injected into a 
    /// rectangle button.
    /// </summary>
    public class MMRectangleButtonSettings : MMSettings
    {
        public MMButtonVisualState<Texture2D> Image;

        public MMButtonVisualState<Color> Color;

        public MMFont Font;

        public MMRectangleButtonSettings()
        {
            this.Image = new MMButtonVisualState<Texture2D>();
        }

        public static MMRectangleButtonSettings Default => new MMRectangleButtonSettings();
    }
}