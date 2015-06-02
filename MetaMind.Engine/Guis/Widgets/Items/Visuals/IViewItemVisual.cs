// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewItemVisual.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.Visuals
{
    using Microsoft.Xna.Framework;

    using IDrawable = MetaMind.Engine.IDrawable;

    public interface IViewItemVisual : IViewItemComponent, IDrawable, IInputable, IUpdateable
    {
    }
}