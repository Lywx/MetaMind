namespace MetaMind.Engine.Entities.Controls.Views.Layouts
{
    using System;
    using Item;

    public interface IMMViewLayout : IMMViewComponent
    {
        void Sort(Func<IMMViewItem, dynamic> key);
    }
}