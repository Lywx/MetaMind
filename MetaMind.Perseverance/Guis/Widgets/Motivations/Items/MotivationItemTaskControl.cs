// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationItemTaskControl.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.Regions;
    using MetaMind.Engine.Guis.Elements.ViewItems;
    using MetaMind.Engine.Guis.Elements.Views;

    using Microsoft.Xna.Framework;

    public class MotivationItemTaskControl : ViewItemComponent
    {
        public MotivationItemTaskControl(IViewItem item)
            : base(item)
        {
        }

        public MotivationTaskTracer TaskTracer { get; private set; }

        public void SelectIt()
        {
            if (this.TaskTracer == null)
            {
                this.TaskTracer = new MotivationTaskTracer(this.ItemControl, new MotivationTaskTracerSettings());
                this.TaskTracer.Load();
            }

            this.TaskTracer.Show();
        }

        public bool UnselectIt()
        {
            if (this.TaskTracer == null)
            {
                return true;
            }

            if (this.TaskTracer.View.IsEnabled(ViewState.View_Has_Focus) && 
                this.TaskTracer.View.Control.Region.IsEnabled(RegionState.Region_Hightlighted))
            {
                return false;
            }
            else
            {
                this.TaskTracer.Close();
                return true;
            }
        }

        public void UpdateStructure(GameTime gameTime)
        {
            if (this.TaskTracer != null)
            {
                this.TaskTracer.UpdateStructure(gameTime);
                if (!this.TaskTracer.View.IsEnabled(ViewState.View_Has_Focus))
                {
                    this.TaskTracer.Close();
                }
            }
        }

        public void UpdateInput(GameTime gameTime)
        {
            if (this.TaskTracer != null )
            {
                this.TaskTracer.UpdateInput(gameTime);
            }
        }
    }
}