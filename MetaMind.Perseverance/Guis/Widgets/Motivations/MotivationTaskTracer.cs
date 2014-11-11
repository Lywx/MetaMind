using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Elements.Regions;
using MetaMind.Engine.Guis.Modules;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations
{
    using MetaMind.Engine.Guis.Elements.Views;

    public class MotivationTaskTracer : Module<MotivationTaskTracerSettings>
    {
        private readonly MotivationItemControl hostControl;

        public MotivationTaskTracer(MotivationItemControl itemControl, MotivationTaskTracerSettings settings)
            : base(settings)
        {
            hostControl = itemControl;

            View = new View(Settings.ViewSettings, Settings.ItemSettings, Settings.ViewFactory);
        }

        public IView View { get; private set; }

        public void Close()
        {
            // clear highlight
            View.Control.Region.Disable(RegionState.Region_Hightlighted);
            View               .Disable(ViewState.View_Active);
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            View.Draw(gameTime, alpha);
        }

        public override void Load()
        {
            foreach (var task in hostControl.ItemData.Tasks)
            {
                View.Control.AddItem(task);
            }
        }

        public void Show()
        {
            // show up tracer
            View.Control.Region.Enable(RegionState.Region_Hightlighted);
            View               .Enable(ViewState.View_Active);
        }

        public override void UpdateInput(GameTime gameTime)
        {
            View.Control.Region.UpdateInput(gameTime);

            // directly update through view control
            if (View.Control.AcceptInput)
            {
                // mouse
                //---------------------------------------------------------------------
                if (InputSequenceManager.Mouse.IsWheelScrolledUp)
                {
                    View.Control.ScrollBar.Trigger();
                    View.Control.Scroll.MoveUp();
                }

                if (InputSequenceManager.Mouse.IsWheelScrolledDown)
                {
                    View.Control.Scroll.MoveDown();
                    View.Control.ScrollBar.Trigger();
                }

                // keyboard
                //---------------------------------------------------------------------
                // movement
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Left))
                {
                    View.Control.MoveLeft();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Right))
                {
                    View.Control.MoveRight();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Up))
                {
                    View.Control.MoveUp();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Down))
                {
                    View.Control.MoveDown();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.SUp))
                {
                    for (var i = 0; i < View.ViewSettings.RowNumDisplay; i++)
                    {
                        View.Control.MoveUp();
                    }
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.SDown))
                {
                    for (var i = 0; i < View.ViewSettings.RowNumDisplay; i++)
                    {
                        View.Control.MoveDown();
                    }
                }

                // escape
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
                {
                    View.Control.Selection.Clear();
                }

                // list management
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TaskCreateItem))
                {
                    var task = View.Control.ItemFactory.CreateData(null);

                    // add to task gui
                    View.Control.AddItem(task);

                    // add to host motivation's tasks
                    hostControl.ItemData.Tasks.Add(task);
                }
            }

            // item input
            //-----------------------------------------------------------------
            if (View.IsEnabled(ViewState.View_Has_Focus))
            {
                foreach (var item in View.Items.ToArray())
                {
                    item.UpdateInput(gameTime);
                }
            }
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            // make sure that task region and task items all follow the host location changes
            View.ViewSettings.StartPoint = Vector2Ext.ToPoint(hostControl.RootFrame.Center.ToVector2() + hostControl.ViewSettings.TracerMargin);

            View.UpdateStructure(gameTime);
        }
    }
}