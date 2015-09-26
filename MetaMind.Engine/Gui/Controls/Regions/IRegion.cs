// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegion.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Regions
{
    using Microsoft.Xna.Framework;
    using IDrawable = Engine.IDrawable;

    public interface IRegion : IRegionElement, IUpdateable, IDrawable, IInputable
    {
    }
}