// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationItemSymbolControl.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class MotivationItemSymbolControl : ViewItemComponent
    {
        public MotivationItemSymbolControl(IViewItem item)
            : base(item)
        {
        }

        public void UpdateStructure(GameTime gameTime)
        {
        }

        public void UpdateInput(GameTime gameTime)
        {
            if (this.Item.IsEnabled(ItemState.Item_Pending))
            {
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Up))
                {
                    this.BecomeWish();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Down))
                {
                    this.BecomeFear();
                }
            }
        }

        public void BecomeWish()
        {
            this.ItemData.Property = "Wish";
        }

        public void BecomeFear()
        {
            this.ItemData.Property = "Fear";
        }
    }
}