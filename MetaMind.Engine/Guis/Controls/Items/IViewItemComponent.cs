namespace MetaMind.Engine.Guis.Controls.Items
{
    using System;
    using Views;

    public interface IViewItemComponent : IViewItemComponentOperations, IGameControllableEntity, IDisposable
    {
        IView View { get; }

        IViewItem Item { get; }
    }
}