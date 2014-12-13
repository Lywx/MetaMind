// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KnowledgeItemResultDataControl.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.ViewItems;

    using Microsoft.Xna.Framework;

    public class KnowledgeItemResultDataControl : ViewItemDataControl
    {
        public KnowledgeItemResultDataControl(IViewItem item)
            : base(item)
        {
        }

        public override void UpdateInput(GameTime gameTime)
        {
            base.UpdateInput(gameTime);

            if (this.Item.IsEnabled(ItemState.Item_Selected))
            {
                // must un-select itself to clear selection or the selection control may 
                // misuse the selection in next search
                this.ItemControl.MouseUnselectsIt();

                this.View.Control.SearchStop();
                this.View.Control.LoadResult(this.ItemData.Name);
            }
        }
    }
}