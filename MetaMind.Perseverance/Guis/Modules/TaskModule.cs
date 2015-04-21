// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskModule.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Modules
{
    using FastMember;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;
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

        public override void Load(IGameInputService input, IGameInteropService interop)
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

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.View.Draw(graphics, time, alpha);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            // mouse
            //-----------------------------------------------------------------
            if (this.View.Control.Active)
            {
                this.View.Control.Region.UpdateInput(time);
            }

            // directly update through view control
            if (this.View.Control.AcceptInput)
            {
                // mouse
                // ---------------------------------------------------------------------
                if (input.State.Mouse.IsWheelScrolledUp)
                {
                    this.View.Control.ScrollBar.Trigger();
                    this.View.Control.Scroll.MoveUp();
                }

                if (input.State.Mouse.IsWheelScrolledDown)
                {
                    this.View.Control.Scroll.MoveDown();
                    this.View.Control.ScrollBar.Trigger();
                }

                // keyboard
                // ---------------------------------------------------------------------
                // movement
                if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Left))
                {
                    // won't trigger scroll bar
                    this.View.Control.Selection.MoveLeft();
                }

                if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Right))
                {
                    // won't trigger scroll bar
                    this.View.Control.Selection.MoveRight();
                }

                if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Up))
                {
                    this.View.Control.MoveUp();
                }

                if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Down))
                {
                    this.View.Control.MoveDown();
                }

                if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastUp))
                {
                    for (var i = 0; i < this.View.ViewSettings.RowNumDisplay; i++)
                    {
                        this.View.Control.MoveUp();
                    }
                }

                if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastDown))
                {
                    for (var i = 0; i < this.View.ViewSettings.RowNumDisplay; i++)
                    {
                        this.View.Control.MoveDown();
                    }
                }

                // escape
                if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Escape))
                {
                    this.View.Control.Selection.Clear();
                }

                // list management
                if (input.State.Keyboard.IsActionTriggered(KeyboardActions.TaskCreateItem))
                {
                    var task = this.View.Control.ItemFactory.CreateData(null);

                    // add to task gui
                    this.View.Control.AddItem(task);

                    // add to host motivation's tasks
                    this.FastHostData["Tasks"].Add(task);
                    
                    // auto-select new item
                    this.View.Control.Selection.Select(this.View.Items.Count - 1);
                }

                if (input.State.Keyboard.IsActionTriggered(KeyboardActions.TaskDeleteItem))
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
                    item.UpdateInput(input, time);
                }
            }
        }

        public override void Update(GameTime time)
        {
            // make sure that task region and task items all follow the host location changes
            this.View.ViewSettings.PointStart = ExtVector2.ToPoint(
                    this.FastHostControl["RootFrame"].Center.ToVector2() + 
                    this.FastHostControl["ViewSettings"].TracerMargin);

            this.View.Update(time);
        }

        #endregion Update and Draw
    }
}