// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemFrameControl.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items
{
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewItemFrameControl : ViewItemComponent, IViewItemFrameControl
    {
        public ViewItemFrameControl(IViewItem item)
            : base(item)
        {
            this.RootFrame = new ItemRootFrame(item) { Size = this.ItemSettings.RootFrameSize };
        }

        public ItemRootFrame RootFrame { get; private set; }

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.RootFrame.UpdateInput(input, time);

            this.Item[ItemState.Item_Is_Mouse_Over]  = this.RootFrame[FrameState.Mouse_Is_Over];
            this.Item[ItemState.Item_Is_Dragging] = this.RootFrame[FrameState.Frame_Is_Dragging];
        }

        public override void Update(GameTime time)
        {
            this.UpdateFrameGeometry();
            this.UpdateFrameLogics();
        }

        protected virtual void UpdateFrameGeometry()
        {
            if (!this.Item[ItemState.Item_Is_Dragging]() && !this.Item[ItemState.Item_Is_Swaping]())
            {
                this.RootFrame.Center = this.ViewControl.Scroll.RootCenterPoint(this.ItemControl.Id);
            }
            else if (this.Item[ItemState.Item_Is_Swaping]())
            {
                this.RootFrame.Center = this.ViewControl.Swap.RootCenterPoint();
            }
        }

        /// <summary>
        /// Updates the item states related to frames.
        /// </summary>
        /// <remarks>
        /// All the frame states change except event type, which should be implemented in custom frame class,
        /// has to be done here to enforce code readability.
        /// </remarks>
        protected virtual void UpdateFrameLogics()
        {
            // frame activation
            if (this.Item[ItemState.Item_Is_Active]() && 
               !this.Item[ItemState.Item_Is_Dragging]())
            {
                this.RootFrame.Enable();
            }
            else if (!this.Item[ItemState.Item_Is_Active]() && 
                     !this.Item[ItemState.Item_Is_Dragging]())
            {
                this.RootFrame.Disable();
            }
        }

        #endregion Update

        public override void Dispose()
        {
            if (this.RootFrame != null)
            {
                this.RootFrame.Dispose();
            }

            base.Dispose();
        }
    }
}