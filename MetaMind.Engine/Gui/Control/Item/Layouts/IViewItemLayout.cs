// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewItemLayout.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Control.Item.Layouts
{
    using System;

    public interface IViewItemLayout : IViewItemComponent 
    {
        int Id { get; set; }

        Func<bool> ItemIsActive { get; }
    }
}