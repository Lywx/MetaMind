namespace MetaMind.Engine.Gui.Controls.Item
{
    using System;
    using Reactors;
    using Views;

    public interface IViewItemComponent : IViewItemComponentOperations, IGameReactor, IMMInputableEntity, IDisposable
    {
        IView View { get; }

        IViewItem Item { get; }
    }
}