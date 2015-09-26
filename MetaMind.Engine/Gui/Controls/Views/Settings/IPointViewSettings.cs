namespace MetaMind.Engine.Gui.Controls.Views.Settings
{
    using Microsoft.Xna.Framework;

    public interface IPointViewSettings : IGameSettings 
    {
        Vector2 ItemMargin { get; }

        Vector2 ViewPosition { get; }

        ViewDirection ViewDirection { get; }
    }
}