﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewVisualComponent .cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Views.Visuals
{
    public abstract class ViewVisualComponent : ViewComponent, IViewVisualComponent
    {
        protected ViewVisualComponent(IView view)
            : base(view)
        {
        }
    }
}