namespace MetaMind.Engine.Guis.Widgets.Items.Layouts
{
    using Data;

    public interface IBlockViewVerticalItemLayout : IPointViewItemLayout
    {
        int BlockRow { get; }

        IBlockViewItemData BlockData { get; }

        /// <summary>
        /// Wrapped block text content
        /// </summary>
        string BlockStringWrapped { get; }
    }
}