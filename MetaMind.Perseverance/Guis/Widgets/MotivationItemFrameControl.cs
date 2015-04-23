namespace MetaMind.Perseverance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class MotivationItemFrameControl : ViewItemFrameControl
    {
        private TimeSpan selectedTime;

        public MotivationItemFrameControl(IViewItem item)
            : base(item)
        {
            this.SymbolFrame = new PickableFrame();
        }

        public override void Dispose()
        {
            try
            {
                if (this.SymbolFrame != null)
                {
                    this.SymbolFrame.Dispose();
                    this.SymbolFrame = null;
                }
            }
            finally
            {
                base.Dispose();
            }
        }

        public PickableFrame SymbolFrame { get; private set; }

        private Point SymbolFrameSize
        {
            get
            {
                return new Point(
                    (int)(this.RootFrame.Width * (1 + this.ItemSettings.SymbolFrameIncrementFactor * Math.Abs(Math.Atan(this.selectedTime.TotalSeconds)))),
                    (int)(this.RootFrame.Height * (1 + this.ItemSettings.SymbolFrameIncrementFactor * Math.Abs(Math.Atan(this.selectedTime.TotalSeconds)))));
            }
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.UpdateFrameSelection(time);
        }

        protected override void UpdateFrameGeometry()
        {
            base.UpdateFrameGeometry();

            this.SymbolFrame.Center = this.RootFrame.Center;
            this.SymbolFrame.Size   = this.SymbolFrameSize;
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            base            .UpdateInput(input, time);
            this.SymbolFrame.UpdateInput(input, time);
        }

        private void UpdateFrameSelection(GameTime gameTime)
        {
            if (this.Item.IsEnabled(ItemState.Item_Selected) &&
               !this.Item.IsEnabled(ItemState.Item_Dragging))
            {
                this.selectedTime = this.selectedTime + gameTime.Times(2);
            }
            else
            {
                if (this.selectedTime.Ticks > 0)
                {
                    this.selectedTime = this.selectedTime - gameTime.Times(5);
                }
                else
                {
                    this.selectedTime = TimeSpan.Zero;
                }
            }
        }
    }
}