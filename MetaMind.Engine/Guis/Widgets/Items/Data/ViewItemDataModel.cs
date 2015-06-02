// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemDataModel.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.Data
{
    using System;
    using System.Reflection;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Events;
    using MetaMind.Engine.Guis.Widgets.Items.Layers;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewItemDataModel : ViewItemComponent, IViewItemDataModel
    {
        private dynamic itemData;

        private string itemDataName;

        public ViewItemDataModel(IViewItem item)
            : base(item)
        {
            this.CharModifier = new ViewItemCharModifier(item);
        }

        ~ViewItemDataModel()
        {
            this.Dispose();
        }

        #region Dependency
        
        protected IViewItemCharModifier CharModifier { get; private set; }

        #endregion

        #region Layering

        public override void SetupLayer()
        {
            var itemLayer = this.ItemGetLayer<ViewItemLayer>();
            this.itemData = itemLayer.ItemData;
        }

        #endregion

        #region IDisposable

        public override void Dispose()
        {
            if (this.CharModifier != null)
            {
                this.CharModifier.Dispose();
            }

            base.Dispose();
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

            this.Item[ItemState.Item_Is_Pending] = () => false;
            this.Item[ItemState.Item_Is_Editing] = () => true;
            this.View[ViewState.View_Is_Editing] = () => true;
 
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
            this.Item[ItemState.Item_Is_Editing] = () => false;
            this.View[ViewState.View_Is_Editing] = () => false;
        }

        private void RefreshInt(object sender, ViewItemDataEventArgs e)
        {
            int result;
            var succeeded = int.TryParse(Font.ContentRegular.PrintableString(e.NewValue), out result);

            this.RefreshValue(this.itemData, succeeded ? result : 0);
        }

        private void RefreshString(object sender, ViewItemDataEventArgs e)
        {
            this.RefreshValue(this.itemData, Font.ContentRegular.PrintableString(e.NewValue));
        }

        private void RefreshValue(dynamic data, dynamic value)
        {
            FieldInfo field = data.GetType().GetField(this.itemDataName);
            if (field != null)
            {
                field.SetValue(data, value);
            }

            PropertyInfo property = data.GetType().GetProperty(this.itemDataName);
            if (property != null)
            {
                property.SetValue(data, value);
            }
        }

        #endregion

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.CharModifier.UpdateInput(input, time);
        }

        #endregion
    }
}