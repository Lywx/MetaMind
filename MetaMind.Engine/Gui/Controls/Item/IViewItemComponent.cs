namespace MetaMind.Engine.Gui.Controls.Item
{
    using System;
    using Entities;
    using Views;

    public interface IViewItemComponent : IViewItemComponentOperations, IMMReactor, IMMInputEntity, IDisposable
    {
        IMMViewNode View { get; }

        IViewItem Item { get; }
    }
}