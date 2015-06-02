// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewItemLayout.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.Layouts
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IViewItemLayout : IViewItemComponent, IUpdateable 
    {
        int Id { get; set; }

        Func<bool> ItemIsActive { get; }
    }
}