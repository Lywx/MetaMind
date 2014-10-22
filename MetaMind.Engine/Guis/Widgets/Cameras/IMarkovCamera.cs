using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Cameras
{
    public interface IMarkovCamera : IWidget
    {
        Vector2 Movement { get; set; }
    }
}