// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegion.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Regions
{
    using Microsoft.Xna.Framework;

    public interface IRegion : IRegionElement, IUpdateable, IMMDrawable, IMMInputable
    {
    }
}