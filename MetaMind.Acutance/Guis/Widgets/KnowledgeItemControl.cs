namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.ViewItems;

    public class KnowledgeItemControl : ViewItemControl2D
    {
        #region Constructors

        public KnowledgeItemControl(IViewItem item)
            : base(item)
        {
            this.ItemFrameControl = new KnowledgeItemFrameControl(item);
        }

        public ItemEntryFrame IdFrame { get { return ((KnowledgeItemFrameControl)this.ItemFrameControl).IdFrame; } }

        public ItemEntryFrame NameFrame { get { return ((KnowledgeItemFrameControl)this.ItemFrameControl).NameFrame; } }

        #endregion Constructors

        #region Update

        public bool Locked
        {
            get
            {
                return this.Item.IsEnabled(ItemState.Item_Editing) || 
                       this.Item.IsEnabled(ItemState.Item_Pending);
            }
        }

        #endregion Update
    }
}