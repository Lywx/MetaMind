namespace MetaMind.Testimony.Guis.Widgets
{
    using System;
    using Engine.Guis.Elements;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Layers;
    using Engine.Guis.Widgets.Items.Layouts;
    using Engine.Guis.Widgets.Items.Settings;
    using Engine.Guis.Widgets.Views.Layers;
    using Engine.Guis.Widgets.Views.Scrolls;
    using Engine.Guis.Widgets.Views.Swaps;
    using Engine.Services;
    using Microsoft.Xna.Framework;

    public class TestItemFrame : BlcokViewVerticalItemFrame
    {
        private ItemSettings itemSettings;

        private IBlockViewVerticalItemLayout itemLayout;

        private IViewSwapController viewSwap;

        private IViewScrollController viewScroll;

        private FrameSettings nameFrameSettings;

        public TestItemFrame(IViewItem item)
            : base(item)
        {
            this.IdFrame = new PickableFrame();
            this.NameFrame = new PickableFrame();
            this.StatusFrame = new PickableFrame();
        }

        ~TestItemFrame()
        {
            this.Dispose();
        }

        #region Layering

        public override void SetupLayer()
        {
            base.SetupLayer();
            
            var viewLayer = this.ViewGetLayer<ViewLayer>();
            var itemLayer = this.ItemGetLayer<TestItemLayer>();

            this.viewScroll = viewLayer.ViewLogic.ViewScroll;
            this.viewSwap = viewLayer.ViewLogic.ViewSwap;

            this.itemSettings = itemLayer.ItemSettings;
            this.itemLayout = itemLayer.ItemLogic.ItemLayout;

            var idFrameSettings = this.itemSettings.Get<FrameSettings>("IdFrame");
            this.nameFrameSettings = this.itemSettings.Get<FrameSettings>("NameFrame");
            var statusFrameSettings = this.itemSettings.Get<FrameSettings>("StatusFrame");

            // id frame - status frame - name frame
            {
                this.IdFrame.Size = idFrameSettings.Size;
                this.IdFrameLocation = this.RootFrameLocation;
            }

            {
                this.StatusFrame.Size = statusFrameSettings.Size;
                this.StatusFrameLocation = () => this.IdFrameLocation() + new Vector2(idFrameSettings.Size.X, 0);
            }

            {
                this.NameFrame.Size = nameFrameSettings.Size;
                this.NameFrameLocation = () => this.StatusFrameLocation() + new Vector2(statusFrameSettings.Size.X, 0);
            }
        }

        #endregion

        #region Frames

        public PickableFrame IdFrame { get; private set; }

        public PickableFrame NameFrame { get; private set; }

        public PickableFrame StatusFrame { get; private set; }

        #endregion

        #region Positions

        public Func<Vector2> NameFrameLocation { get; protected set; }

        public Func<Vector2> IdFrameLocation { get; protected set; }

        public Func<Vector2> StatusFrameLocation { get; protected set; }

        #endregion

        public override void Dispose()
        {
            if (this.NameFrame != null)
            {
                this.NameFrame.Dispose();
            }

            if (this.IdFrame != null)
            {
                this.IdFrame.Dispose();
            }

            if (this.StatusFrame != null)
            {
                this.StatusFrame.Dispose();
            }

            base.Dispose();
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            base.UpdateInput(input, time);

            this.IdFrame.UpdateInput(input, time);
            this.NameFrame.UpdateInput(input, time);
            this.StatusFrame.UpdateInput(input, time);
        }

        protected override void UpdateFrameGeometry()
        {
            this.RootFrame.Location = this.RootFrameLocation().ToPoint();
            this.IdFrame.Location = this.IdFrameLocation().ToPoint();
            this.NameFrame.Location  = this.NameFrameLocation().ToPoint();
            this.StatusFrame.Location = this.StatusFrameLocation().ToPoint();

            this.RootFrame.Size = new Point(this.nameFrameSettings.Size.X, this.itemLayout.BlockRow * this.nameFrameSettings.Size.Y);
            this.NameFrame.Size = new Point(this.nameFrameSettings.Size.X, this.itemLayout.BlockRow * this.nameFrameSettings.Size.Y);
        }

        public override void Update(GameTime time)
        {
            this.RootFrame.Update(time);
            this.IdFrame.Update(time);
            this.NameFrame.Update(time);
            this.StatusFrame.Update(time);

            this.UpdateFrameGeometry();
            this.UpdateFrameStates();
        }
    }
}