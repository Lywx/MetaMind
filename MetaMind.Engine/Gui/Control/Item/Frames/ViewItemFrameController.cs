// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemFrame.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Control.Item.Frames
{
    using System;
    using Element;
    using Layers;
    using Microsoft.Xna.Framework;
    using Service;

    public abstract class ViewItemFrameController : ViewItemComponent, IViewItemFrameController
    {
        public ViewItemFrameController(IViewItem item, ViewItemRectangle itemRootRectangle)
            : base(item)
        {
            if (itemRootRectangle == null)
            {
                throw new ArgumentNullException(nameof(itemRootRectangle));
            }

            this.RootRectangle = itemRootRectangle;

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

            var rootFrameSettings = itemSettings.Get<FrameSettings>("RootFrame");
            this.RootRectangle.Size = rootFrameSettings.Size;
        }

        private void RegisterStates()
        {
            this.Item[ItemState.Item_Is_Mouse_Over] = this.RootRectangle[ElementState.Mouse_Is_Over];
            this.Item[ItemState.Item_Is_Dragging] = this.RootRectangle[ElementState.Element_Is_Dragging];
        }

        #endregion

        #region Dependency

        public ViewItemRectangle RootRectangle { get; private set; }

        #endregion

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.RootRectangle.UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.RootRectangle.Update(time);

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
                        this.RootRectangle?.Dispose();
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