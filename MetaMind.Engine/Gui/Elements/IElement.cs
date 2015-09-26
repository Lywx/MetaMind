namespace MetaMind.Engine.Gui.Elements
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IElement
    {
        #region States

        bool Active { get; set; }

        #endregion

        #region Position

        Point Center { get; set; }

        Point Location { get; set; }

        int X { get; set; }

        int Y { get; set; }

        #endregion

        #region Geometry

        Point Size { get; set; }

        int Width { get; set; }

        int Height { get; set; }

        Rectangle Bounds { get; set; }

        #endregion

        #region Events

        event EventHandler<ElementEventArgs> Move;

        event EventHandler<ElementEventArgs> Resize;

        #endregion
    }
}