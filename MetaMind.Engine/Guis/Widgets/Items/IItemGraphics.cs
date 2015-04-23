﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IItemGraphics.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items
{
    using Microsoft.Xna.Framework;

    using IDrawable = MetaMind.Engine.IDrawable;

    public interface IItemGraphics : IDrawable, IInputable, IUpdateable
    {
    }
}