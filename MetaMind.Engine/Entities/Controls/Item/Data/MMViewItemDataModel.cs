// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemDataModel.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Entities.Controls.Item.Data
{
    using System;
    using System.Reflection;
    using Components.Content.Fonts;
    using Layers;
    using Microsoft.Xna.Framework;
    using Views;

    public class MMViewItemDataModel : MMViewItemControllerComponent, IMMViewItemDataModel
    {
        private dynamic itemData;

        private string itemDataName;

        #region Constructors and Finalizer

        public MMViewItemDataModel(IMMViewItem item)
            : base(item)
        {
            this.CharModifier = new MMViewItemCharModifier(item);
        }

        ~MMViewItemDataModel()
        {
            this.Dispose(true);
        }

        #endregion

        #region Dependency
        
        protected IMMViewItemCharModifier CharModifier { get; private set; }

        protected MMFont NSimSumRegularFont { get; set; }

        #endregion

        #region Initialization

        public override void Initialize()
        {
            var itemLayer = this.GetItemLayer<MMViewItemLayer>();
            this.itemData = itemLayer.ItemData;

            this.LoadContent();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            this.NSimSumRegularFont = this.GlobalInterop.Asset.Fonts["NSimSum Regular"];
        }

        #endregion

        #region Edit and Refresh

        public void EditCancel()
        {
            this.CharModifier.Cancel();
        }

        public void EditInt(string targetName)
        {
            this.EditStart(targetName, false);

            this.CharModifier.ValueModified += this.RefreshInt;
            this.CharModifier.ModificationEnded += this.EditTerminate;
        }

        public void EditString(string targetName)
        {
            this.EditStart(targetName, true);

            this.CharModifier.ValueModified += this.RefreshString;
            this.CharModifier.ModificationEnded += this.EditTerminate;
        }

        private void EditStart(string targetName, bool showCursor)
        {
            this.itemDataName = targetName;

            this.Item[MMViewItemState.Item_Is_Pending] = () => false;
            this.Item[MMViewItemState.Item_Is_Editing] = () => true;
            this.View[MMViewState.View_Is_Editing] = () => true;
 
            FieldInfo field = this.itemData.GetType().GetField(targetName);
            if (field != null)
            {
                this.CharModifier.Initialize(field.GetValue(this.itemData).ToString(), showCursor);
            }

            PropertyInfo property = this.itemData.GetType().GetProperty(targetName);
            if (property != null)
            {
                this.CharModifier.Initialize(property.GetValue(this.itemData).ToString(), showCursor);
            }
        }

        private void EditTerminate(object sender, EventArgs e)
        {
            this.Item[MMViewItemState.Item_Is_Editing] = () => false;
            this.View[MMViewState.View_Is_Editing] = () => false;
        }

        private void RefreshInt(object sender, MMViewItemDataEventArgs e)
        {
            int result;
            var succeeded = int.TryParse(this.NSimSumRegularFont.AvailableString(e.NewValue), out result);

            this.RefreshValue(this.itemData, succeeded ? result : 0);
        }

        private void RefreshString(object sender, MMViewItemDataEventArgs e)
        {
            this.RefreshValue(this.itemData, this.NSimSumRegularFont.AvailableString(e.NewValue));
        }

        private void RefreshValue(dynamic data, dynamic value)
        {
            FieldInfo field = data.GetType().GetField(this.itemDataName);
            field?.SetValue(data, value);

            PropertyInfo property = data.GetType().GetProperty(this.itemDataName);
            property?.SetValue(data, value);
        }

        #endregion

        #region Update

        public override void UpdateInput(GameTime time)
        {
            this.CharModifier.UpdateInput(time);
        }

        #endregion

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
                        this.CharModifier?.Dispose();
                        this.CharModifier = null;
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