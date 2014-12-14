namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class KnowledgeItemCallDataControl : ViewItemDataControl
    {
        public KnowledgeItemCallDataControl(IViewItem item)
            : base(item)
        {
        }

        public override void UpdateInput(GameTime gameTime)
        {
            base.UpdateInput(gameTime);

            if (Item.IsEnabled(ItemState.Item_Selected))
            {
                // must un-select itself to clear selection or the selection control may 
                // misuse the selection in next frame
                ItemControl.MouseUnselectsIt();

                View.Control.LoadCall(ItemData.CallName, ItemData.Path, ItemData.Minutes);
            }
        }
    }
}