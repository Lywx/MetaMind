namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;

    public interface IView : IViewObject
    {
        dynamic Control { get; set; }

        IViewGraphics Graphics { get; set; }

        List<IViewItem> Items { get; set; }
    }
}