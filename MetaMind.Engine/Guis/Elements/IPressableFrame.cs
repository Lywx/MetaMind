namespace MetaMind.Engine.Guis.Elements
{
    using System;

    using Microsoft.Xna.Framework;

    public interface IPressableFrame : IFrameEntity, IPressable, IDisposable
    {
        Point Center { get; set; }

        Point Size { get; set; }

        Point Location { get; set; }

        int X { get; set; }

        int Y { get; set; }

        int Width { get; set; }

        int Height { get; set; }

        Rectangle Rectangle { get; set; }
    }
}