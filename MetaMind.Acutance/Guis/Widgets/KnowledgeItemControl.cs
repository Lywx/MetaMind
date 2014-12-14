// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KnowledgeItemControl.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class KnowledgeItemControl : ViewItemControl2D
    {
        #region Constructors

        public KnowledgeItemControl(IViewItem item)
            : base(item)
        {
            this.ItemFrameControl = new KnowledgeItemFrameControl(item);

            if (ItemData.IsFile)
            {
                this.ItemDataControl = new KnowledgeItemFileDataControl(item);
            }
            else if (ItemData.IsSearchResult)
            {
                this.NameFrame.MouseLeftClicked += this.LoadResult;
            }
            else if (ItemData.IsCall)
            {
                this.NameFrame.MouseLeftDoubleClicked += this.LoadCall;
            }
        }

        #endregion Constructors

        public ItemEntryFrame IdFrame
        {
            get { return ((KnowledgeItemFrameControl)this.ItemFrameControl).IdFrame; }
        }

        public ItemEntryFrame NameFrame
        {
            get { return ((KnowledgeItemFrameControl)this.ItemFrameControl).NameFrame; }
        }

        #region Events

        private void LoadCall(object sender, FrameEventArgs e)
        {
            View.Control.LoadCall(ItemData.CallName, ItemData.Path, ItemData.Minutes);
        }

        private void LoadResult(object sender, FrameEventArgs frameEventArgs)
        {
            View.Control.SearchStop();
            View.Control.LoadResult(ItemData.Name, true);
        }

        #endregion

        #region Update

        public bool Locked
        {
            get { return this.Item.IsEnabled(ItemState.Item_Editing) || this.Item.IsEnabled(ItemState.Item_Pending); }
        }

        public override void UpdateInput(GameTime gameTime)
        {
            // mouse and keyboard in modifier
            // -----------------------------------------------------------------
            base.UpdateInput(gameTime);

            // keyboard
            // -----------------------------------------------------------------
            if (ViewSettings.KeyboardEnabled)
            {
                if (this.AcceptInput)
                {
                    // in pending status
                    if (this.Item.IsEnabled(ItemState.Item_Pending))
                    {
                        if (InputSequenceManager.Keyboard.IsKeyTriggered(Keys.N))
                        {
                            // pre-trim to keep consistent prompt display
                            var fileDataControl = this.ItemDataControl as KnowledgeItemFileDataControl;
                            if (fileDataControl != null)
                            {
                                fileDataControl.TrimPrompt();
                            }

                            this.ItemDataControl.EditString("Name");

                            // compensate for pre-trim
                            if (fileDataControl != null)
                            {
                                fileDataControl.SetName(ItemData.Name);
                            }
                        }

                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
                        {
                            this.View.Disable(ViewState.Item_Editting);
                            this.Item.Disable(ItemState.Item_Pending);
                        }
                    }

                    if (!this.Locked)
                    {
                        // normal status
                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.KnowledgeEditItem))
                        {
                            this.View.Enable(ViewState.Item_Editting);
                            this.Item.Enable(ItemState.Item_Pending);
                        }
                    }
                }
            }
        }

        #endregion Update

        #region Operations

        public void EditCancel()
        {
            ItemDataControl.EditCancel();
        }

        public void SetName(string fileName)
        {
            var itemDataControl = this.ItemDataControl as KnowledgeItemFileDataControl;
            if (itemDataControl != null)
            {
                itemDataControl.SetName(fileName);
            }
        }

        #endregion
    }
}