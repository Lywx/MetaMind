namespace MetaMind.Engine.Guis.Controls.Views.Settings
{
    using Microsoft.Xna.Framework;

    public interface IPointViewSettings : IControlSettings 
    {
        Vector2 ItemMargin { get; }

        Vector2 ViewPosition { get; }

        ViewDirection ViewDirection { get; }
    }
}