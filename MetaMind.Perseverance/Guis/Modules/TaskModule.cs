// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskModule.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Modules
{
    using FastMember;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Perseverance.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public class TaskModule : Module<TaskModuleSettings>
    {
        private bool loadFinished;

        private int  loadIndex;

        public TaskModule(MotivationItemControl itemControl, TaskModuleSettings settings)
            : base(settings)
        {
            this.View = new PointView(this.Settings.ViewSettings, this.Settings.ItemSettings, this.Settings.ViewFactory, this);

            // faster dynamic accessors with fast member
            this.FastHostControl = ObjectAccessor.Create(itemControl);
            this.FastHostData    = ObjectAccessor.Create(this.FastHostControl["ItemData"]);
        }

        public IView View { get; private set; }

        public dynamic FastHostData { get; set; }

        private dynamic FastHostControl { get; set; }

        public void Close()
        {
            this.View.Disable(ViewState.View_Active);
            this.View.Disable(ViewState.View_Has_Focus);
        }

        public void Show()
        {
            this.View.Enable(ViewState.View_Active);

            // trigger selection to focus
            int? current  = this.View.Control.Selection.SelectedId;
            int? previous = this.View.Control.Selection.PreviousSelectedId;
            if (current  != null || 
                previous != null)
            {
                this.View.Control.Selection.Select(current != null ? (int)current : (int)previous);
            }
            else
            {
                this.View.Control.Selection.Select(0);
            }
        }

        public override void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio)
        {
            // performance penalty due to dynamic type
            // performance is still bad even with fast member
            if (!this.loadFinished && this.FastHostData["Tasks"].Count != 0)
            {
                this.View.Control.AddItem(this.FastHostData["Tasks"][this.loadIndex]);

                this.loadIndex++;
            }

            if (this.loadIndex >= this.FastHostData["Tasks"].Count)
            {
                this.loadFinished = true;
            }
        }

        #region Update and Draw

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            this.View.Draw(gameTime, alpha);
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            // mouse
            //-----------------------------------------------------------------
            if (this.View.Control.Active)
            {
                this.View.Control.Region.UpdateInput(gameTime);
            }

            // directly update through view control
            if (this.View.Control.AcceptInput)
            {
                // mouse
                // ---------------------------------------------------------------------
                if (InputSequenceManager.Mouse.IsWheelScrolledUp)
                {
                    this.View.Control.ScrollBar.Trigger();
                    this.View.Control.Scroll.MoveUp();
                }

                if (InputSequenceManager.Mouse.IsWheelScrolledDown)
                {
                    this.View.Control.Scroll.MoveDown();
                    this.View.Control.ScrollBar.Trigger();
                }

                // keyboard
                // ---------------------------------------------------------------------
                // movement
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Left))
                {
                    // won't trigger scroll bar
                    this.View.Control.Selection.MoveLeft();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Right))
                {
                    // won't trigger scroll bar
                    this.View.Control.Selection.MoveRight();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Up))
                {
                    this.View.Control.MoveUp();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Down))
                {
                    this.View.Control.MoveDown();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.FastUp))
                {
                    for (var i = 0; i < this.View.ViewSettings.RowNumDisplay; i++)
                    {
                        this.View.Control.MoveUp();
                    }
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.FastDown))
                {
                    for (var i = 0; i < this.View.ViewSettings.RowNumDisplay; i++)
                    {
                        this.View.Control.MoveDown();
                    }
                }

                // escape
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
                {
                    this.View.Control.Selection.Clear();
                }

                // list management
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TaskCreateItem))
                {
                    var task = this.View.Control.ItemFactory.CreateData(null);

                    // add to task gui
                    this.View.Control.AddItem(task);

                    // add to host motivation's tasks
                    this.FastHostData["Tasks"].Add(task);
                    
                    // auto-select new item
                    this.View.Control.Selection.Select(this.View.Items.Count - 1);
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TaskDeleteItem))
                {
                    // item deletion was processed by item control which is unaware of motivation

                    // get the deleted item data
                    var id = (int)this.View.Control.Selection.SelectedId;
                    if (id < this.View.Items.Count)
                    {
                        var task = this.View.Items[id].ItemData;

                        // remove from host motivation's tasks
                        this.FastHostData["Tasks"].Remove(task);
                    }

                    // itme deletion is handled by item control
                    // auto select last item
                    if (this.View.Items.Count > 1)
                    {
                        // this will be called before item deletion
                        if (this.View.Control.Selection.SelectedId != null && 
                            this.View.Control.Selection.SelectedId > View.Items.Count - 2)
                        {
                            this.View.Control.Selection.Select(View.Items.Count - 2);
                        }
                    }
                }
            }

            // item input
            // -----------------------------------------------------------------
            if (this.View.IsEnabled(ViewState.View_Has_Focus))
            {
                foreach (var item in this.View.Items.ToArray())
                {
                    item.UpdateInput(gameTime);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            // make sure that task region and task items all follow the host location changes
            this.View.ViewSettings.PointStart = ExtVector2.ToPoint(
                    this.FastHostControl["RootFrame"].Center.ToVector2() + 
                    this.FastHostControl["ViewSettings"].TracerMargin);

            this.View.Update(gameTime);
        }

        #endregion Update and Draw
    }
}