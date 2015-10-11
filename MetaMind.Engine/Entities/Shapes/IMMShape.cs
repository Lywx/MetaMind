namespace MetaMind.Engine.Entities.Shapes
{
    using System;
    using Entities.Elements;
    using Microsoft.Xna.Framework;

    public interface IMMShape
    {
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

        event EventHandler<MMElementEventArgs> Move;

        event EventHandler<MMElementEventArgs> Resize;

        #endregion
    }
}