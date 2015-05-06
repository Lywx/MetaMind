// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemFrame.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.Frames
{
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Items.Layers;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public abstract class ViewItemFrame : ViewItemComponent, IViewItemFrame
    {
        public ViewItemFrame(IViewItem item)
            : base(item)
        {
            this.RootFrame = new ViewItemRootFrame(item) { Size = this.ItemGetLayer<ViewItemLayer>().ItemSettings.Get<FrameSettings>("RootFrame").Size };

            this.Item[ItemState.Item_Is_Mouse_Over]  = this.RootFrame[FrameState.Mouse_Is_Over];
            this.Item[ItemState.Item_Is_Dragging]    = this.RootFrame[FrameState.Frame_Is_Dragging];
        }

        #region Dependency

        public IViewItemRootFrame RootFrame { get; private set; }

        #endregion

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.RootFrame.UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            this.UpdateFrameGeometry();
            this.UpdateFrameStates();
        }

        protected abstract void UpdateFrameGeometry();

        /// <summary>
        /// Updates frame states related to items.
        /// </summary>
        protected virtual void UpdateFrameStates()
        {
            if (this.Item[ItemState.Item_Is_Active]() && 
               !this.Item[ItemState.Item_Is_Dragging]())
            {
                this.RootFrame.IsActive = true;
            }
            else if (!this.Item[ItemState.Item_Is_Active]() && 
                     !this.Item[ItemState.Item_Is_Dragging]())
            {
                this.RootFrame.IsActive = false;
            }
        }

        #endregion Update

        #region IDisposable

        public override void Dispose()
        {
            if (this.RootFrame != null)
            {
                this.RootFrame.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}