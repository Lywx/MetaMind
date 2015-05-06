namespace MetaMind.Engine.Guis.Widgets.Items.Factories
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Logic;
    using MetaMind.Engine.Guis.Widgets.Items.Visuals;

    public interface IViewItemFactory
    {
        Func<IViewItem, dynamic> Data { get; set; }

        Func<IViewItem, IViewItemLogic> Logic { get; set; }

        Func<IViewItem, IViewItemVisual> Visual { get; set; }

        dynamic CreateData(IViewItem item);

        IViewItemLogic CreateLogic(IViewItem item);

        IViewItemVisual CreateVisual(IViewItem item);
    }
}