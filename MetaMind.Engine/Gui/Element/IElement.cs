namespace MetaMind.Engine.Gui.Element
{
    using Microsoft.Xna.Framework;

    public interface IElement
    {
        #region States

        bool Active { get; set; }

        #endregion

        #region Geometry

        Point Center { get; set; }

        Point Size { get; set; }

        Point Location { get; set; }

        int X { get; set; }

        int Y { get; set; }

        int Width { get; set; }

        int Height { get; set; }

        Rectangle Bounds { get; set; }

        #endregion
    }
}