namespace MetaMind.Engine.Guis.Widgets.Items.Data
{
    using System;

    public class BlockViewVerticalItemData : IBlockViewVerticalItemData
    {
        public BlockViewVerticalItemData(string blockText)
        {
            if (blockText == null)
            {
                throw new ArgumentNullException("blockText");
            }

            this.BlockText = blockText;
        }

        public string BlockText { get; private set; }
    }
}