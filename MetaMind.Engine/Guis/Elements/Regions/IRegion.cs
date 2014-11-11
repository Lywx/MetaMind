using MetaMind.Engine.Guis.Elements.Frames;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Elements.Regions
{
    public interface IRegion
    {
        IPickableFrame Frame { get; set; }
        bool[] States { get; }

        int X { get; set; }
        int Y { get; set; }
        int Height { get; set; }
        int Width { get; set; }

        void UpdateInput(GameTime gameTime);

        bool IsEnabled(RegionState state);
    }
}