namespace MetaMind.Testimony.Guis.Widgets.IndexViews.Tests
{
    using System;
    using Engine.Guis.Elements;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Layouts;
    using Engine.Guis.Widgets.Items.Settings;
    using Engine.Services;
    using Microsoft.Xna.Framework;

    public class TestItemFrame : BlcokViewVerticalItemFrame
    {
        private ItemSettings itemSettings;

        private IBlockViewVerticalItemLayout itemLayout;

        private FrameSettings nameFrameSettings;

        private FrameSettings descriptionFrameSettings;

        public TestItemFrame(IViewItem item)
            : base(item)
        {
            this.IdFrame = new PickableFrame();
            this.StatusFrame = new PickableFrame();
            this.StatisticsFrame = new PickableFrame();
            this.PlusFrame = new PickableFrame();

            this.NameFrame = new PickableFrame();
            this.DescriptionFrame = new PickableFrame();
        }

        ~TestItemFrame()
        {
            this.Dispose();
        }

        #region Layering

        public override void SetupLayer()
        {
            base.SetupLayer();
            
            var itemLayer = this.ItemGetLayer<TestItemLayer>();

            this.itemSettings = itemLayer.ItemSettings;
            this.itemLayout = itemLayer.ItemLogic.ItemLayout;

            var idFrameSettings = this.itemSettings.Get<FrameSettings>("IdFrame");
            var statusFrameSettings = this.itemSettings.Get<FrameSettings>("StatusFrame");
            var statisticsFrameSettings = this.itemSettings.Get<FrameSettings>("StatisticsFrame");
            var plusFrameSettings = this.itemSettings.Get<FrameSettings>("PlusFrame");

            this.nameFrameSettings = this.itemSettings.Get<FrameSettings>("NameFrame");
            this.descriptionFrameSettings = this.itemSettings.Get<FrameSettings>("DescriptionFrame");

            // —————————————————————————————————————————    
            // id frame | status frame     | name frame
            // —————————————————————————————————————————    
            //          | statistics frame | description frame       
            // —————————————————————————————————————————    
            {
                this.IdFrame.Size = idFrameSettings.Size;
                this.IdFrameLocation = this.RootFrameLocation;
            }

            {
                this.StatusFrame.Size = statusFrameSettings.Size;
                this.StatusFrameLocation = () => this.IdFrameLocation() + new Vector2(idFrameSettings.Size.X, 0);
            }

            {
                this.StatisticsFrame.Size = statisticsFrameSettings.Size;
                this.StatisticsFrameLocation = () => this.StatusFrameLocation() + new Vector2(0, statusFrameSettings.Size.Y);
            }

            {
                this.PlusFrame.Size = plusFrameSettings.Size;
                this.PlusFrameLocation = () => this.IdFrameLocation() + new Vector2(0, idFrameSettings.Size.Y);
            }

            {
                this.NameFrame.Size = this.nameFrameSettings.Size;
                this.NameFrameLocation = () => this.StatusFrameLocation() + new Vector2(statusFrameSettings.Size.X, 0);
            }

            {
                this.DescriptionFrame.Size = this.descriptionFrameSettings.Size;
                this.DescriptionFrameLocation = () => this.NameFrameLocation() + new Vector2(0, this.nameFrameSettings.Size.Y);
            }
        }

        #endregion

        #region Frames

        public PickableFrame IdFrame { get; private set; }

        public PickableFrame PlusFrame { get; set; }

        public PickableFrame NameFrame { get; private set; }

        public PickableFrame DescriptionFrame { get; private set; }

        public PickableFrame StatusFrame { get; private set; }

        public PickableFrame StatisticsFrame { get; set; }

        #endregion

        #region Positions

        public Func<Vector2> IdFrameLocation { get; protected set; }
        public Func<Vector2> PlusFrameLocation { get; set; }

        public Func<Vector2> StatusFrameLocation { get; protected set; }

        public Func<Vector2> StatisticsFrameLocation { get; set; }

        public Func<Vector2> NameFrameLocation { get; protected set; }

        public Func<Vector2> DescriptionFrameLocation { get; protected set; }

        #endregion

        public override void Dispose()
        {
            if (this.IdFrame != null)
            {
                this.IdFrame.Dispose();
            }

            if (this.PlusFrame != null)
            {
                this.PlusFrame.Dispose();
            }

            if (this.StatusFrame != null)
            {
                this.StatusFrame.Dispose();
            }

            if (this.StatisticsFrame != null)
            {
                this.StatisticsFrame.Dispose();
            }

            if (this.NameFrame != null)
            {
                this.NameFrame.Dispose();
            }

            if (this.DescriptionFrame != null)
            {
                this.DescriptionFrame.Dispose();
            }

            base.Dispose();
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            base.UpdateInput(input, time);

            this.IdFrame.UpdateInput(input, time);
            this.PlusFrame.UpdateInput(input, time);

            this.StatusFrame.UpdateInput(input, time);
            this.StatisticsFrame.UpdateInput(input, time);

            this.NameFrame.UpdateInput(input, time);
            this.DescriptionFrame.UpdateInput(input, time);
        }

        protected override void UpdateFrameGeometry()
        {
            this.RootFrame.Location = this.RootFrameLocation().ToPoint();

            this.IdFrame.Location = this.IdFrameLocation().ToPoint();
            this.PlusFrame.Location = this.PlusFrameLocation().ToPoint();

            this.StatusFrame.Location = this.StatusFrameLocation().ToPoint();
            this.StatisticsFrame.Location = this.StatisticsFrameLocation().ToPoint();

            this.NameFrame.Location  = this.NameFrameLocation().ToPoint();
            this.DescriptionFrame.Location  = this.DescriptionFrameLocation().ToPoint();

            this.RootFrame.Size = new Point(
                this.RootFrame.Size.X,
                this.itemLayout.BlockRow * this.descriptionFrameSettings.Size.Y); 

            this.DescriptionFrame.Size = new Point(
                this.descriptionFrameSettings.Size.X,

                // this.itemLayout.BlockRow - 1 for taken position of name frame 
                (this.itemLayout.BlockRow - 1) * this.descriptionFrameSettings.Size.Y);
        }

        public override void Update(GameTime time)
        {
            this.RootFrame.Update(time);

            this.IdFrame.Update(time);
            this.PlusFrame.Update(time);

            this.StatusFrame.Update(time);
            this.StatisticsFrame.Update(time);

            this.NameFrame.Update(time);
            this.DescriptionFrame.Update(time);

            this.UpdateFrameGeometry();
            this.UpdateFrameStates();
        }
    }
}