// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegion.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Regions
{
    using MetaMind.Engine.Guis.Elements;

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

        void Update(IGameInput gameInput, GameTime gameTime);
    }
}