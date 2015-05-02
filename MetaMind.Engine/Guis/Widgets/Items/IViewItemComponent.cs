namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    public interface IViewItemComponent : IDisposable
    {
        IViewItem Item { get; }

        IView View { get; }
    }
}