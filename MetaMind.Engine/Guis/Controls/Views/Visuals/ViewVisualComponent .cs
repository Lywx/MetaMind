// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewVisualComponent .cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Controls.Views.Visuals
{
    public abstract class ViewVisualComponent : ViewComponent, IViewVisualComponent
    {
        protected ViewVisualComponent(IView view)
            : base(view)
        {
        }
    }
}