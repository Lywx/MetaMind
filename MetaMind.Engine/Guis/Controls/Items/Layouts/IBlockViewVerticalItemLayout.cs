namespace MetaMind.Engine.Guis.Controls.Items.Layouts
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