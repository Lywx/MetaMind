namespace MetaMind.Engine.Core.Entity.Control.Views.Layouts
{
    using System;
    using Item;

    public interface IMMViewLayout : IMMViewComponent
    {
        void Sort(Func<IMMViewItem, dynamic> key);
    }
}