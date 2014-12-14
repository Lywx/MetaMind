// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KnowledgeItemResultDataControl.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;

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

            if (Item.IsEnabled(ItemState.Item_Selected))
            {
                // must un-select itself to clear selection or the selection control may 
                // misuse the selection in next search
                ItemControl.MouseUnselectsIt();

                View.Control.SearchStop();
                View.Control.LoadResult(ItemData.Name);
            }
        }
    }
}