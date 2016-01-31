namespace MetaMind.Engine.Core.Entity.Control.Views.Scrolls
{
    using System;
    using Entity.Common;
    using Entity.Input;
    using Microsoft.Xna.Framework;

    public class ViewVerticalScrollbarVisual : MMVisualEntity
    {
        private readonly ViewVerticalScrollbar scrollbar;

        private readonly ImageBox scrollbarShape; 
        
        private int scrollbarBrightness;

        public ViewVerticalScrollbarVisual(ViewVerticalScrollbar scrollbar)
        {
            if (scrollbar == null)
            {
                throw new ArgumentNullException("scrollbar");
            }

            this.scrollbar = scrollbar;
            this.scrollbarShape = new ImageBox(
                () => this.scrollbar.Rectangle,
                () => this.scrollbar[MMElementState.Element_Is_Dragging]()
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
            base.Update(time);

            var brightness = (int)(this.ScrollbarSettings.ColorBrightnessFadeSpeed * time.ElapsedGameTime.TotalSeconds);

            if (this.scrollbar[MMElementState.Mouse_Is_Over]() ||
                this.scrollbar[MMElementState.Element_Is_Dragging]())
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

        public override void Draw(GameTime time)
        {
            this.scrollbarShape.Draw(time);
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