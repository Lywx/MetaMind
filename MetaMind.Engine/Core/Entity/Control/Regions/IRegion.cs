// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegion.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Core.Entity.Control.Regions
{
    using Entity.Common;
    using Microsoft.Xna.Framework;

    public interface IRegion : IRegionElement, IUpdateable, IMMDrawable, IMMInputtable
    {
    }
}