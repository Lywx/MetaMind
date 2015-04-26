// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KnowledgeItemControl.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

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
            else if (ItemData.IsResult)
            {
                this.NameFrame.MouseLeftClicked += this.LoadKnowledge;
            }
        }

        /// <remarks>
        /// Don't need to remove delegate LoadKnowledge, for NameFrame may be diposed by
        /// ItemFrameControl.
        /// </remarks>>
        ~KnowledgeItemControl()
        {
            this.Dispose();
        }

        #endregion Constructors

        #region Public Properties

        public PickableFrame IdFrame
        {
            get
            {
                return ((KnowledgeItemFrameControl)this.ItemFrameControl).IdFrame;
            }
        }

        public PickableFrame NameFrame
        {
            get
            {
                return ((KnowledgeItemFrameControl)this.ItemFrameControl).NameFrame;
            }
        }

        #endregion

        #region Events

        private void LoadKnowledge(object sender, FrameEventArgs frameEventArgs)
        {
            ViewControl.SearchStop();

            // load knowledge file from the first line
            ViewControl.LoadResult(ItemData.Name, true, 0);
        }

        #endregion

        #region Update

        public bool Locked
        {
            get { return this.Item.IsEnabled(ItemState.Item_Is_Editing) || this.Item[ItemState.Item_Is_Pending]() }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            // mouse and keyboard in modifier
            // -----------------------------------------------------------------
            base.UpdateInput(input, time);

            // keyboard
            // -----------------------------------------------------------------
            if (ViewSettings.KeyboardEnabled)
            {
                if (this.AcceptInput)
                {
                    // in pending status
                    if (this.Item.IsEnabled(ItemState.Item_Is_Pending))
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

                        if (InputSequenceManager.Keyboard.IsActionTriggered(KeyboardActions.Escape))
                        {
                            this.View.Disable(ViewState.View_Is_Editing);
                            this.Item.Disable(ItemState.Item_Is_Pending);
                        }
                    }

                    if (!this.Locked)
                    {
                        // normal status
                        if (InputSequenceManager.Keyboard.IsActionTriggered(KeyboardActions.KnowledgeEditItem))
                        {
                            this.View[View.State.View_Editting] = () => true;
                            this.Item.Enable(ItemState.Item_Is_Pending);
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