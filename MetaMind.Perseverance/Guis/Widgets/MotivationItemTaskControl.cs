// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationItemTaskControl.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;
    using MetaMind.Perseverance.Guis.Modules;

    using Microsoft.Xna.Framework;

    public class MotivationItemTaskControl : ViewItemComponent
    {
        public MotivationItemTaskControl(IViewItem item)
            : base(item)
        {
        }

        public TaskModule TaskModule { get; private set; }

        public void SelectsIt()
        {
            if (this.TaskModule == null)
            {
                Point start = ItemControl.RootFrame.Center + ViewSettings.TracerMargin;

                this.TaskModule = new TaskModule(this.ItemControl, new TaskModuleSettings(start));
                this.TaskModule.LoadContent(this.GameInterop);
            }

            this.TaskModule.Show();
        }

        public bool UnselectsIt()
        {
            if (this.TaskModule == null)
            {
                return true;
            }

            // FIXME: Bad design here
            if (this.TaskModule.View               .IsEnabled(ViewState.View_Has_Focus) && 
                this.TaskModule.View.Control.Region.IsEnabled(RegionState.Region_Has_Focus))
            {
                return false;
            }
            else
            {
                this.TaskModule.Close();
                return true;
            }
        }

        public override void Update(GameTime time)
        {
            if (this.TaskModule != null)
            {
                this.TaskModule.Update(time);
                this.TaskModule.LoadContent(this.GameInterop);

                if (!this.TaskModule.View.IsEnabled(ViewState.View_Has_Focus))
                {
                    this.TaskModule.Close();
                }
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (this.TaskModule != null )
            {
                this.TaskModule.UpdateInput(input, time);
            }
        }
    }
}