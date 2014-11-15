// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskViewScrollBar.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    using System;

    using C3.Primtive2DXna;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;

    using Microsoft.Xna.Framework;

    public class TaskViewScrollBar : ViewComponent
    {
        private readonly TaskViewScrollBarSettings settings;

        private          int                       alpha;

        public TaskViewScrollBar(IView view, TaskViewSettings viewSettings, TaskItemSettings itemSettings, TaskViewScrollBarSettings scrollBarSettings)
            : base(view, viewSettings, itemSettings)
        {
            this.settings = scrollBarSettings;
        }

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
                    this.settings.Width,
                    ViewControl.Region.Height * ViewSettings.RowNumDisplay / ViewControl.RowNum);
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (ViewControl.RowNum > ViewSettings.RowNumDisplay)
            {
                Primitives2D.FillRectangle(
                    ScreenManager.SpriteBatch,
                    this.ScrollBarRectangle,
                    this.settings.Color.MakeTransparent((byte)this.alpha));
            }
        }

        public void Trigger()
        {
            this.alpha = this.settings.BrightnessMax;
        }

        public void Update(GameTime gameTime)
        {
            this.alpha -= this.settings.BrightnessTransitionRate;
            if (this.alpha < 0)
            {
                this.alpha = 0;
            }
        }
    }
}