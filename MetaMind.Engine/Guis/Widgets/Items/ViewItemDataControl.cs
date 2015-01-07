// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemDataControl.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;
    using System.Reflection;

    using MetaMind.Engine.Concepts;
    using MetaMind.Engine.Events;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class ViewItemDataControl : ViewItemComponent
    {
        private string dataName;

        public ViewItemDataControl(IViewItem item)
            : base(item)
        {
            this.CharModifier = new ViewItemCharModifier(item);
        }

        ~ViewItemDataControl()
        {
            this.Dispose();
        }

        public override void Dispose()
        {
            try
            {
                if (this.CharModifier != null)
                {
                    this.CharModifier.Dispose();
                }

                this.CharModifier = null;
            }
            finally
            {
                base.Dispose();
            }
        }

        protected IViewItemCharModifier CharModifier { get; private set; }

        public void EditExperience(string targetName)
        {
            this.EditStart(targetName, false);

            this.CharModifier.ValueModified += this.RefreshEditingExperience;
            this.CharModifier.ModificationEnded += this.TerminateEditing;
        }

        public void EditInt(string targetName)
        {
            this.EditStart(targetName, false);

            this.CharModifier.ValueModified += this.RefreshEditingInt;
            this.CharModifier.ModificationEnded += this.TerminateEditing;
        }
        
        public void EditString(string targetName)
        {
            this.EditStart(targetName, true);

            this.CharModifier.ValueModified += this.RefreshEditingString;
            this.CharModifier.ModificationEnded += this.TerminateEditing;
        }

        public void EditCancel()
        {
           this.CharModifier.Cancel(); 
        }

        public virtual void UpdateInput(GameTime gameTime)
        {
            this.CharModifier.UpdateInput(gameTime);
        }

        public virtual void UpdateStructure(GameTime gameTime)
        {
        }

        private void EditStart(string targetName, bool showCursor)
        {
            this.dataName = targetName;

            Item.Disable(ItemState.Item_Pending);
            Item.Enable(ItemState.Item_Editing);
            View.Enable(ViewState.Item_Editting);

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

        private void RefreshEditingExperience(object sender, ViewItemDataEventArgs e)
        {
            string inputString = FontManager.GetDisaplayableCharacters(ItemSettings.NameFont, e.NewValue);

            // parse input to experience
            int integer;
            var succeded = int.TryParse(inputString, out integer);
            var experience = new Experience(DateTime.Now, TimeSpan.FromHours(integer), DateTime.Now);

            this.RefreshValue(ItemData, succeded ? experience : Experience.Zero);
        }

        private void RefreshEditingInt(object sender, ViewItemDataEventArgs e)
        {
            string inputString = FontManager.GetDisaplayableCharacters(ItemSettings.NameFont, e.NewValue);

            // parse input to int
            int result;
            var succeded = int.TryParse(inputString, out result);

            this.RefreshValue(ItemData, succeded ? result : 0);
        }

        private void RefreshEditingString(object sender, ViewItemDataEventArgs e)
        {
            // make sure name is exactly the same as the displayed name
            string inputString = FontManager.GetDisaplayableCharacters(ItemSettings.NameFont, e.NewValue);

            this.RefreshValue(ItemData, inputString);
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

        private void TerminateEditing(object sender, EventArgs e)
        {
            Item.Disable(ItemState.Item_Editing);
            View.Disable(ViewState.Item_Editting);
        }
    }
}