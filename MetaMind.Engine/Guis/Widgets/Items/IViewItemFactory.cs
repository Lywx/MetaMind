namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    public interface IViewItemFactory
    {
        Func<IViewItem, dynamic> Data { get; set; }

        Func<IViewItem, dynamic> Logic { get; set; }

        Func<IViewItem, dynamic> Visual { get; set; }

        dynamic CreateData(IViewItem item);

        dynamic CreateLogic(IViewItem item);

        IItemVisual CreateVisual(IViewItem item);
    }
}