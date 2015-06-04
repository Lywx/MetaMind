namespace MetaMind.Engine.Guis.Widgets.Items.Data
{
    public interface IBlockViewVerticalItemData
    {
        /// <summary>
        /// Raw text content to display on the block.
        /// </summary>
        string BlockStringRaw { get; }

        string BlockLabel { get; }

        string BlockFrame { get; }
    }
}
