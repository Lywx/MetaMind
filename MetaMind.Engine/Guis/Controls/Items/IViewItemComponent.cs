namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;
    using Views;

    public interface IViewItemComponent : IViewItemComponentOperations, IGameControllableEntity, IDisposable
    {
        IView View { get; }

        IViewItem Item { get; }
    }
}