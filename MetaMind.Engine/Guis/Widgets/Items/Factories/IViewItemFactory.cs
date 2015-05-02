namespace MetaMind.Engine.Guis.Widgets.Items.Factories
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Visuals;

    public interface IViewItemFactory
    {
        Func<IViewItem, dynamic> Data { get; set; }

        Func<IViewItem, dynamic> Logic { get; set; }

        Func<IViewItem, dynamic> Visual { get; set; }

        dynamic CreateData(IViewItem item);

        dynamic CreateLogic(IViewItem item);

        IViewItemVisual CreateVisual(IViewItem item);
    }
}