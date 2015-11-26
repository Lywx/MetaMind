// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewItemLayout.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Entities.Controls.Item.Layouts
{
    using System;

    public interface IMMViewItemLayout : IMMViewItemControllerComponent
    {
        int Id { get; set; }

        Func<bool> ItemIsActive { get; }
    }
}