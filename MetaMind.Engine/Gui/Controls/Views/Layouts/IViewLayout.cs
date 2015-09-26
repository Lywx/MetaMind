namespace MetaMind.Engine.Gui.Controls.Views.Layouts
{
    using System;
    using Item;

    public interface IViewLayout : IViewComponent
    {
        void Sort(Func<IViewItem, dynamic> key);
    }
}