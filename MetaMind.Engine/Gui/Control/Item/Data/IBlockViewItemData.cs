namespace MetaMind.Engine.Gui.Control.Item.Data
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
