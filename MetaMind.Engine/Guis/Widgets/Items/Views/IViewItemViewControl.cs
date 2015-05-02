namespace MetaMind.Engine.Guis.Widgets.Items.Views
{
    using System;

    using Microsoft.Xna.Framework;

    public interface IViewItemViewControl : IViewItemComponent, IUpdateable, IDisposable
    {
        Func<bool> ItemIsActive { get; }

        void PassInViewLogic();

        void ViewSelect();

        void ViewUnselect();
    }
}