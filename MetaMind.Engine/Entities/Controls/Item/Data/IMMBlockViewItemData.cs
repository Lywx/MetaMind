namespace MetaMind.Engine.Entities.Controls.Item.Data
{
    public interface IMMBlockViewItemData
    {
        /// <summary>
        /// Raw text content to display on the block.
        /// </summary>
        string BlockStringRaw { get; }

        string BlockLabel { get; }

        string BlockFrame { get; }
    }
}
