namespace MetaMind.Runtime.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ExperienceItemFrameControl : ViewItemFrameControl
    {
        public ExperienceItemFrameControl(IViewItem item)
            : base(item)
        {
            this.NameFrame  = new PickableFrame();
            this.LHoldFrame = new PickableFrame();
            this.RHoldFrame = new PickableFrame();

            this.RootFrameLocation = () =>
                {
                    if (!this.Item[ItemState.Item_Is_Dragging]() && 
                        !this.Item[ItemState.Item_Is_Swaping]())
                    {
                        IContinousViewScrollControl scroll = this.ViewControl.Scroll;

                        return scroll.RootCenterPoint(this.ItemControl.Id).ToVector2() + new Vector2(0, this.ItemSettings.IdFrameSize.Y);
                    }

                    if (this.Item[ItemState.Item_Is_Swaping]())
                    {
                        return this.ViewControl.Swap.RootCenterPoint().ToVector2() + new Vector2(0, this.ItemSettings.IdFrameSize.Y);
                    }

                    return this.RootFrame.Location.ToVector2();
                };

            this.NameFrameLocation = this.RootFrameLocation;

            this.LHolderFrameLocation = () => { };
            this.RHolderFrameLocation = () => { };
        }

        ~ExperienceItemFrameControl()
        {
            this.Dispose();
        }

        public PickableFrame LHoldFrame { get; private set; }

        public PickableFrame NameFrame { get; private set; }

        public PickableFrame RHoldFrame { get; private set; }

        protected Func<Vector2> LHolderFrameLocation { get; set; }

        protected Func<Vector2> NameFrameLocation { get; set; }

        protected Func<Vector2> RHolderFrameLocation { get; set; }

        protected Func<Vector2> RootFrameLocation { get; set; }

        public override void Dispose()
        {
            if (this.NameFrame != null)
            {
                this.NameFrame.Dispose();
            }

            if (this.LHoldFrame != null)
            {
                this.LHoldFrame.Dispose();
            }

            if (this.RHoldFrame != null)
            {
                this.RHoldFrame.Dispose();
            }

            base.Dispose();
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            base.UpdateInput(input, time);

            this.NameFrame .UpdateInput(input, time);
            this.LHoldFrame.UpdateInput(input, time);
            this.RHoldFrame.UpdateInput(input, time);
        }

        protected override void UpdateFrameGeometry()
        {
            ((ExperienceItemSettings)this.ItemSettings).ExperienceFrameSize.X = middleWidth;
            ((ExperienceItemSettings)this.ItemSettings).ProgressFrameSize.X   = middleWidth;

            this.RootFrame.Location = this.RootFrameLocation().ToPoint();
            this.NameFrame.Location = this.NameFrameLocation().ToPoint();
            this.LHoldFrame.Location = this.LHolderFrameLocation().ToPoint();
            this.RHoldFrame.Location = this.RHolderFrameLocation().ToPoint();

            this.RootFrame.Size = this.ItemSettings.RootFrameSize;
            this.NameFrame.Size = this.ItemSettings.NameFrameSize;
            this.LHoldFrame.Size = this.ItemSettings.LeftHolderFrameSize;
            this.RHoldFrame.Size = this.ItemSettings.RightHolderFrameSize;
        }
    }

    public interface IContinousViewScrollControl
    {
    }
}