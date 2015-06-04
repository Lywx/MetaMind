namespace MetaMind.Engine.Guis.Widgets.Items.Layouts
{
    using Data;

    public interface IBlockViewVerticalItemLayout : IPointViewItemLayout
    {
        int BlockRow { get; }

        IBlockViewVerticalItemData BlockData { get; }

        /// <summary>
        /// Wrapped block text content
        /// </summary>
        string BlockStringWrapped { get; }
    }
}