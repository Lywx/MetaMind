// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationTaskTracer.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets.Motivations
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Regions;
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Media;

    public class MotivationTaskTracer : Module<MotivationTaskTracerSettings>
    {
        private readonly MotivationItemControl hostControl;

        public MotivationTaskTracer(MotivationItemControl itemControl, MotivationTaskTracerSettings settings)
            : base(settings)
        {
            this.hostControl = itemControl;

            this.View = new View(this.Settings.ViewSettings, this.Settings.ItemSettings, this.Settings.ViewFactory);
        }

        public IView View { get; private set; }

        public void Close()
        {
            // clear highlight
            this.View.Disable(ViewState.View_Active);
            this.View.Disable(ViewState.View_Has_Focus);
            this.View.Control.Selection.Clear();
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.View.Draw(gameTime, alpha);
        }

        public override void Load()
        {
            foreach (var task in this.hostControl.ItemData.Tasks)
            {
                this.View.Control.AddItem(task);
            }
        }

        public void Show()
        {
            // show up tracer
            this.View.Enable(ViewState.View_Active);
            this.View.Control.Selection.Select(0);
        }

        public override void UpdateInput(GameTime gameTime)
        {
            // mouse
            //-----------------------------------------------------------------
            if (View.Control.Active)
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
                    this.View.Control.MoveLeft();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Right))
                {
                    this.View.Control.MoveRight();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Up))
                {
                    this.View.Control.MoveUp();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Down))
                {
                    this.View.Control.MoveDown();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.SUp))
                {
                    for (var i = 0; i < this.View.ViewSettings.RowNumDisplay; i++)
                    {
                        this.View.Control.MoveUp();
                    }
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.SDown))
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
                    this.hostControl.ItemData.Tasks.Add(task);
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

        public override void UpdateStructure(GameTime gameTime)
        {
            // make sure that task region and task items all follow the host location changes
            this.View.ViewSettings.StartPoint = Vector2Ext.ToPoint(this.hostControl.RootFrame.Center.ToVector2() + this.hostControl.ViewSettings.TracerMargin);

            this.View.UpdateStructure(gameTime);
        }
    }
}