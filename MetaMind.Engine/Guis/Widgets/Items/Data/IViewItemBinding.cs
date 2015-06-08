namespace MetaMind.Engine.Guis.Widgets.Items.Data
{
    using System.Collections.Generic;

    public interface IViewItemBinding<TData>
    {
        TData AddData(IViewItem item);

        TData RemoveData(IViewItem item);

        IList<TData> AllData();
    }
}