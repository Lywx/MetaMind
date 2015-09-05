namespace MetaMind.Engine.Guis.Widgets.Views.Layouts
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;

    public interface IViewLayout : IViewComponent
    {
        void Sort(Func<IViewItem, dynamic> key);
    }
}