namespace MetaMind.Engine.Guis.Controls.Views.Layouts
{
    using System;
    using Items;

    public interface IViewLayout : IViewComponent
    {
        void Sort(Func<IViewItem, dynamic> key);
    }
}