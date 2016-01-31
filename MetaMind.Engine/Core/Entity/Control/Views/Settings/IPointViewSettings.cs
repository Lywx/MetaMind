namespace MetaMind.Engine.Core.Entity.Control.Views.Settings
{
    using Core.Settings;
    using Microsoft.Xna.Framework;

    public interface IPointViewSettings : IMMSettings 
    {
        Vector2 ItemMargin { get; }

        Vector2 ViewPosition { get; }

        ViewDirection ViewDirection { get; }
    }
}