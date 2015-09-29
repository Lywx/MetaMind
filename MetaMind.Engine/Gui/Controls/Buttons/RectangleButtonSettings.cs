namespace MetaMind.Engine.Gui.Controls.Buttons
{
    using Elements.Rectangles;
    using Engine.Components.Content.Fonts;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Rectangle button settings that could be consumed and injected into a 
    /// rectangle button.
    /// </summary>
    public class RectangleButtonSettings : GameSettings
    {
        public ButtonVisualState<Texture2D> Image;

        public ButtonVisualState<Color> Color;

        public Font Font;
    }
}