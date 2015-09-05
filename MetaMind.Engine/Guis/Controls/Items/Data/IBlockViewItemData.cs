namespace MetaMind.Engine.Guis.Controls.Items.Data
{
    public interface IBlockViewItemData
    {
        /// <summary>
        /// Raw text content to display on the block.
        /// </summary>
        string BlockStringRaw { get; }

        string BlockLabel { get; }

        string BlockFrame { get; }
    }
}
