// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewItemLayout.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Item.Layouts
{
    using System;

    public interface IViewItemLayout : IViewItemComponent 
    {
        int Id { get; set; }

        Func<bool> ItemIsActive { get; }
    }
}