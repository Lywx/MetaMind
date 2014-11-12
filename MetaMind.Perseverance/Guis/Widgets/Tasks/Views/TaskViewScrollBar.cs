// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskViewScrollBar.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    using C3.Primtive2DXna;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
    using Microsoft.Xna.Framework;
    using System;

    public class TaskViewScrollBar : ViewComponent
    {
        private byte alpha;

        public TaskViewScrollBar(IView view, TaskViewSettings viewSettings, TaskItemSettings itemSettings, TaskViewScrollBarSettings scrollBarSettings)
            : base(view, viewSettings, itemSettings)
        {
            this.Settings = scrollBarSettings;
            this.alpha = 0;
        }

        public TaskViewScrollBarSettings Settings { get; private set; }

        private Rectangle ScrollBarRectangle
        {
            get
            {
                var distance = ViewControl.Region.Height * (float)ViewControl.Scroll.YOffset
                               / (ViewControl.RowNum - ViewSettings.RowNumDisplay)
                               * (1 - (float)ViewSettings.RowNumDisplay / ViewControl.RowNum);

                // top boundary reaches the top of the ViewRegion
                // bottom boundary reaches the bottom of the ViewRegion
                return new Rectangle(
                    ViewControl.Region.X + ViewControl.Region.Width,
                    ViewControl.Region.Y + (int)Math.Ceiling(distance),
                    this.Settings.Width,
                    ViewControl.Region.Height * ViewSettings.RowNumDisplay / ViewControl.RowNum);
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (ViewControl.RowNum > ViewSettings.RowNumDisplay)
            {
                Primitives2D.FillRectangle(ScreenManager.SpriteBatch, this.ScrollBarRectangle, this.Settings.Color.MakeTransparent(this.alpha));
            }
        }

        public void Trigger()
        {
            this.alpha = this.Settings.BrightnessMax;
        }

        public void Update(GameTime gameTime)
        {
            if (this.alpha > 2)
            {
                this.alpha -= this.Settings.BrightnessTransitionRate;
            }
            else
            {
                this.alpha = 0;
            }
        }
    }
}