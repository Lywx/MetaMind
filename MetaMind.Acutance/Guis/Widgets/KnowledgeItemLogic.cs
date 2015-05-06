// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KnowledgeItemLogic.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Items.Logic;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class KnowledgeItemLogic : PointView2DItemLogic
    {
        #region Constructors

        public KnowledgeItemLogic(IViewItem item)
            : base(item)
        {
            this.ItemFrame = new KnowledgeItemFrameControl(item);

            if (ItemData.IsFile)
            {
                this.ItemModel = new KnowledgeItemFileDataControl(item);
            }
            else if (ItemData.IsResult)
            {
                this.NameFrame.MouseLeftPressed += this.LoadKnowledge;
            }
        }

        ~KnowledgeItemLogic()
        {
            this.Dispose();
        }

        #endregion Constructors

        #region Public Properties

        public PickableFrame IdFrame
        {
            get
            {
                return ((KnowledgeItemFrameControl)this.ItemFrame).IdFrame;
            }
        }

        public PickableFrame NameFrame
        {
            get
            {
                return ((KnowledgeItemFrameControl)this.ItemFrame).NameFrame;
            }
        }

        #endregion

        #region Events

        private void LoadKnowledge(object sender, FrameEventArgs frameEventArgs)
        {
            this.ViewLogic.SearchStop();

            // load knowledge file from the first line
            this.ViewLogic.LoadResult(ItemData.Name, true, 0);
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
                            var fileDataControl = this.ItemModel as KnowledgeItemFileDataControl;
                            if (fileDataControl != null)
                            {
                                fileDataControl.TrimPrompt();
                            }

                            this.ItemModel.EditString("Name");

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
            this.ItemModel.EditCancel();
        }

        public void SetName(string fileName)
        {
            var itemDataControl = this.ItemModel as KnowledgeItemFileDataControl;
            if (itemDataControl != null)
            {
                itemDataControl.SetName(fileName);
            }
        }

        #endregion
    }
}