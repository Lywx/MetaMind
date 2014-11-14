// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegion.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements.Regions
{
    using MetaMind.Engine.Guis.Elements.Frames;

    using Microsoft.Xna.Framework;

    public interface IRegion
    {
        IPickableFrame Frame { get; set; }

        int Height { get; set; }

        bool[] States { get; }

        int Width { get; set; }

        int X { get; set; }

        int Y { get; set; }

        bool IsEnabled(RegionState state);

        void UpdateInput(GameTime gameTime);
    }
}