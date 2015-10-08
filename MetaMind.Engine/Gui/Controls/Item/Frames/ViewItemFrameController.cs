// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemFrame.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Item.Frames
{
    using System;
    using Elements;
    using Layers;
    using Microsoft.Xna.Framework;
    using Services;

    public abstract class ViewItemFrameController : ViewItemComponent, IViewItemFrameController
    {
        public ViewItemFrameController(IViewItem item, ViewItemImmRectangle itemImmRootRectangle)
            : base(item)
        {
            if (itemImmRootRectangle == null)
            {
                throw new ArgumentNullException(nameof(itemImmRootRectangle));
            }

            this.RootImmRectangle = itemImmRootRectangle;

            this.RegisterStates();
        }

        ~ViewItemFrameController()
        {
            this.Dispose(true);
        }

        #region Initialization

        public override void Initialize()
        {
            var itemLayer = this.GetItemLayer<ViewItemLayer>();
            var itemSettings = itemLayer.ItemSettings;

            var rootFrameSettings = itemSettings.Get<ViewItemVisualSettings>("RootFrame");
            this.RootImmRectangle.Size = rootFrameSettings.Size;
        }

        private void RegisterStates()
        {
            this.Item[ViewItemState.Item_Is_Mouse_Over] = this.RootImmRectangle[MMElementState.Mouse_Is_Over];
            this.Item[ViewItemState.Item_Is_Dragging] = this.RootImmRectangle[MMElementState.Element_Is_Dragging];
        }

        #endregion

        #region Dependency

        public ViewItemImmRectangle RootImmRectangle { get; private set; }

        #endregion

        #region Update

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
        {
            this.RootImmRectangle.UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.RootImmRectangle.Update(time);

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
                        this.RootImmRectangle?.Dispose();
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