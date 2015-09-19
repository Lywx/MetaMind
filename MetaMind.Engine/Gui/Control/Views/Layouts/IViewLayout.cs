namespace MetaMind.Engine.Gui.Control.Views.Layouts
{
    using System;
    using Item;

    public interface IViewLayout : IViewComponent
    {
        void Sort(Func<IViewItem, dynamic> key);
    }
}