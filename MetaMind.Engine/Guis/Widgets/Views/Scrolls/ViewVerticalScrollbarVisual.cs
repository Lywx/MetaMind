namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using System;
    using Elements;
    using Microsoft.Xna.Framework;
    using Services;
    using Widgets.Visuals;

    public class ViewVerticalScrollbarVisual : GameVisualEntity
    {
        private readonly ViewVerticalScrollbar scrollbar;

        private readonly Box scrollbarShape; 
        
        private int scrollbarBrightness;

        public ViewVerticalScrollbarVisual(ViewVerticalScrollbar scrollbar)
        {
            if (scrollbar == null)
            {
                throw new ArgumentNullException("scrollbar");
            }

            this.scrollbar = scrollbar;
            this.scrollbarShape = new Box(
                () => this.scrollbar.Rectangle,
                () => this.scrollbar[FrameState.Frame_Is_Dragging]()
                        ? this.ScrollbarSettings.DraggingColor
                        : this.ScrollbarSettings.RegularColor,
                () => true);
        }

        private ViewScrollbarSettings ScrollbarSettings
        {
            get { return this.scrollbar.ScrollbarSettings; }
        }

        #region Update

        public override void Update(GameTime time)
        {
            var brightness = (int)(this.ScrollbarSettings.ColorBrightnessFadeSpeed * time.ElapsedGameTime.TotalSeconds);

            if (this.scrollbar[FrameState.Mouse_Is_Over]() ||
                this.scrollbar[FrameState.Frame_Is_Dragging]())
            {
                this.scrollbarBrightness += brightness;
                if (this.scrollbarBrightness > this.ScrollbarSettings.ColorBrightnessMax)
                {
                    this.scrollbarBrightness = this.ScrollbarSettings.ColorBrightnessMax;
                }
            }
            else
            {
                this.scrollbarBrightness -= brightness;
                if (this.scrollbarBrightness < this.ScrollbarSettings.ColorBrightnessMin)
                {
                    this.scrollbarBrightness = this.ScrollbarSettings.ColorBrightnessMin;
                }
            }
        }

        #endregion

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.scrollbarShape.Draw(graphics, time, Math.Min(alpha, (byte)this.scrollbarBrightness));
        }

        #endregion

        #region Operations

        public void Toggle()
        {
            this.scrollbarBrightness = this.ScrollbarSettings.ColorBrightnessMax;
        }

        #endregion
    }
}