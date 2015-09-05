// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegion.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Controls.Regions
{
    using Elements;
    using Microsoft.Xna.Framework;
    using IDrawable = Engine.IDrawable;

    public interface IRegion : IUpdateable, IDrawable, IInputable, IRegionEntity
    {
        IPickableFrame Frame { get; set; }

        int Height { get; set; }

        int Width { get; set; }

        int X { get; set; }

        int Y { get; set; }
    }
}