// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemDataModifier.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;
    using System.Reflection;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Events;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewItemDataModifier : ViewItemComponent
    {
        private string dataName;

        public ViewItemDataModifier(IViewItem item)
            : base(item)
        {
            this.CharModifier = new ViewItemCharModifier(item);
        }

        ~ViewItemDataModifier()
        {
            this.Dispose();
        }

        protected IViewItemCharModifier CharModifier { get; private set; }

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
            this.dataName = targetName;

            Item[ItemState.Item_Is_Pending] = () => false;
            Item[ItemState.Item_Is_Editing] = () => true;
            View[ViewState.View_Is_Editing] = () => true;

            FieldInfo field = ItemData.GetType().GetField(targetName);
            if (field != null)
            {
                this.CharModifier.Initialize(field.GetValue(ItemData).ToString(), showCursor);
            }

            PropertyInfo property = ItemData.GetType().GetProperty(targetName);
            if (property != null)
            {
                this.CharModifier.Initialize(property.GetValue(ItemData).ToString(), showCursor);
            }
        }

        private void EditTerminate(object sender, EventArgs e)
        {
            Item[ItemState.Item_Is_Editing]  = () => false;
            View[ViewState.View_Is_Editing] = () => false;
        }

        private void RefreshInt(object sender, ViewItemDataEventArgs e)
        {
            int result;
            var succeded = int.TryParse(Font.ContentRegular.PrintableString(e.NewValue), out result);

            this.RefreshValue(ItemData, succeded ? result : 0);
        }

        private void RefreshString(object sender, ViewItemDataEventArgs e)
        {
            this.RefreshValue(this.ItemData, Font.ContentRegular.PrintableString(e.NewValue));
        }

        private void RefreshValue(dynamic itemData, dynamic value)
        {
            FieldInfo field = itemData.GetType().GetField(this.dataName);
            if (field != null)
            {
                field.SetValue(itemData, value);
            }

            PropertyInfo property = itemData.GetType().GetProperty(this.dataName);
            if (property != null)
            {
                property.SetValue(itemData, value);
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