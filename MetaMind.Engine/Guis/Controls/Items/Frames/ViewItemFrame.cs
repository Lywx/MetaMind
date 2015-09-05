// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemFrame.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.Frames
{
    using System;
    using Elements;
    using Layers;
    using Services;

    using Microsoft.Xna.Framework;

    public abstract class ViewItemFrame : ViewItemComponent, IViewItemFrame
    {
        public ViewItemFrame(IViewItem item, IViewItemRootFrame itemRootFrame)
            : base(item)
        {
            if (itemRootFrame == null)
            {
                throw new ArgumentNullException(nameof(itemRootFrame));
            }

            this.RootFrame = itemRootFrame;

            this.Item[ItemState.Item_Is_Mouse_Over] = this.RootFrame[FrameState.Mouse_Is_Over];
            this.Item[ItemState.Item_Is_Dragging]   = this.RootFrame[FrameState.Frame_Is_Dragging];
        }

        ~ViewItemFrame()
        {
            this.Dispose(true);
        }

        public override void SetupLayer()
        {
            var itemLayer = this.ItemGetLayer<ViewItemLayer>();
            var itemSettings = itemLayer.ItemSettings;

            var rootFrameSettings = itemSettings.Get<FrameSettings>("RootFrame");
            this.RootFrame.Size = rootFrameSettings.Size;
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
            base.Update(time);

            this.RootFrame.Update(time);

            this.UpdateFrameGeometry();
            this.UpdateFrameStates();
        }

        protected abstract void UpdateFrameGeometry();

        protected virtual void UpdateFrameStates() {}

        #endregion Update

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        this.RootFrame?.Dispose();
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}