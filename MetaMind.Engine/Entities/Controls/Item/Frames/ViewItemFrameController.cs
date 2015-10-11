// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemFrame.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Entities.Controls.Item.Frames
{
    using System;
    using Entities.Elements;
    using Layers;
    using Microsoft.Xna.Framework;

    public abstract class MMViewItemFrameController : MMViewItemControllerComponent, IMMViewItemFrameController
    {
        public MMViewItemFrameController(IMMViewItem item, ViewItemImmRectangle itemImmRootRectangle)
            : base(item)
        {
            if (itemImmRootRectangle == null)
            {
                throw new ArgumentNullException(nameof(itemImmRootRectangle));
            }

            this.RootImmRectangle = itemImmRootRectangle;

            this.RegisterStates();
        }

        ~MMViewItemFrameController()
        {
            this.Dispose(true);
        }

        #region Initialization

        public override void Initialize()
        {
            var itemLayer = this.GetItemLayer<MMViewItemLayer>();
            var itemSettings = itemLayer.ItemSettings;

            var rootFrameSettings = itemSettings.Get<MMViewItemRenderSettings>("RootFrame");
            this.RootImmRectangle.Size = rootFrameSettings.Size;
        }

        private void RegisterStates()
        {
            this.Item[MMViewItemState.Item_Is_Mouse_Over] = this.RootImmRectangle[MMElementState.Mouse_Is_Over];
            this.Item[MMViewItemState.Item_Is_Dragging] = this.RootImmRectangle[MMElementState.Element_Is_Dragging];
        }

        #endregion

        #region Dependency

        public ViewItemImmRectangle RootImmRectangle { get; private set; }

        #endregion

        #region Update

        public override void UpdateInput(GameTime time)
        {
            this.RootImmRectangle.UpdateInput(time);
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