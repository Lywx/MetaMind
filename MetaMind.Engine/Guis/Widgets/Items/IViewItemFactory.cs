namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    public interface IViewItemFactory
    {
        Func<IViewItem, dynamic> Data { get; set; }

        dynamic CreateData(IViewItem item);

        dynamic CreateLogicControl(IViewItem item);

        IItemVisualControl CreateVisualControl(IViewItem item);
    }
}