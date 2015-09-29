namespace MetaMind.Engine.Gui.Controls.Item
{
    using System;
    using Components;
    using Views;

    public interface IViewItemComponent : IViewItemComponentOperations, IComponent, IGameInputableEntity, IDisposable
    {
        IView View { get; }

        IViewItem Item { get; }
    }
}