namespace MetaMind.Engine.Gui.Controls.Views.Settings
{
    using Engine.Settings;
    using Microsoft.Xna.Framework;

    public interface IPointViewSettings : IMMSettings 
    {
        Vector2 ItemMargin { get; }

        Vector2 ViewPosition { get; }

        ViewDirection ViewDirection { get; }
    }
}