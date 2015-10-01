namespace MetaMind.Engine.Gui.Controls.Item
{
    using System;
    using Entities;
    using Reactors;
    using Views;

    public interface IViewItemComponent : IViewItemComponentOperations, IMMReactor, IMMInputableEntity, IDisposable
    {
        IView View { get; }

        IViewItem Item { get; }
    }
}