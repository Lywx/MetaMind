namespace MetaMind.Engine.Guis.Widgets.Items.Data
{
    using System.Collections.Generic;

    public interface IViewItemBinding
    {
        dynamic AddData(IViewItem item);

        dynamic RemoveData(IViewItem item);

        IList<dynamic> AllData();
    }
}