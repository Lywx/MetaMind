// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewVisualComponent .cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    public class ViewVisualComponent : ViewComponent, IViewVisualComponent
    {
        protected ViewVisualComponent(IView view, ICloneable viewSettings, ICloneable itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }
    }
}