namespace MetaMind.Engine.Guis.Widgets.Items.Data
{
    using System.Collections.Generic;

    public interface IViewBinding 
    {
        dynamic AddData(IViewItem item);

        dynamic RemoveData(IViewItem item);

        /// <summary>
        /// Gets the covariant list.
        /// </summary>
        IReadOnlyList<object> AllData { get; }
    }
}