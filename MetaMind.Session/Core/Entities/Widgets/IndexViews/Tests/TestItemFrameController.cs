namespace MetaMind.Session.Guis.Widgets.IndexViews.Tests
{
    using System;
    using Engine.Core.Entity.Control.Item;
    using Engine.Core.Entity.Control.Item.Frames;
    using Engine.Core.Entity.Control.Item.Layouts;
    using Engine.Core.Entity.Control.Item.Settings;
    using Engine.Core.Entity.Input;
    using Microsoft.Xna.Framework;

    public class TestItemFrameController : MMBlockViewVerticalItemFrameController
    {
        private ItemSettings itemSettings;

        private IMMBlockViewVerticalItemLayout itemLayout;

        private MMViewItemRenderSettings nameFrameSettings;

        private MMViewItemRenderSettings descriptionFrameSettings;

        public TestItemFrameController(IMMViewItem item, ViewItemImmRectangle itemImmRootRectangle)
            : base(item, itemImmRootRectangle)
        {
            this.IdImmRectangle = new MMPickableRectangleElement();
            this.StatusImmRectangle = new MMPickableRectangleElement();
            this.StatisticsImmRectangle = new MMPickableRectangleElement();
            this.PlusImmRectangle = new MMPickableRectangleElement();

            this.NameImmRectangle = new MMPickableRectangleElement();
            this.DescriptionImmRectangle = new MMPickableRectangleElement();
        }

        ~TestItemFrameController()
        {
            this.Dispose();
        }

        #region Layering

        public override void Initialize()
        {
            base.Initialize();
            
            var itemLayer = this.GetItemLayer<TestItemLayer>();

            this.itemSettings = itemLayer.ItemSettings;
            this.itemLayout = itemLayer.ItemLogic.ItemLayout;

            var idFrameSettings = this.itemSettings.Get<MMViewItemRenderSettings>("IdFrame");
            var statusFrameSettings = this.itemSettings.Get<MMViewItemRenderSettings>("StatusFrame");
            var statisticsFrameSettings = this.itemSettings.Get<MMViewItemRenderSettings>("StatisticsFrame");
            var plusFrameSettings = this.itemSettings.Get<MMViewItemRenderSettings>("PlusFrame");

            this.nameFrameSettings = this.itemSettings.Get<MMViewItemRenderSettings>("NameFrame");
            this.descriptionFrameSettings = this.itemSettings.Get<MMViewItemRenderSettings>("DescriptionFrame");

            // +----------+------------------+-------------------+
            // | id frame | status frame     | name frame        |
            // +----------+------------------+-------------------+
            // |          | statistics frame | description frame |
            // +----------+------------------+-------------------+  
            {
                this.IdImmRectangle.Size = idFrameSettings.Size;
                this.IdFrameLocation = this.RootFrameLocation;
            }

            {
                this.StatusImmRectangle.Size = statusFrameSettings.Size;
                this.StatusFrameLocation = () => this.IdFrameLocation() + new Vector2(idFrameSettings.Size.X, 0);
            }

            {
                this.StatisticsImmRectangle.Size = statisticsFrameSettings.Size;
                this.StatisticsFrameLocation = () => this.StatusFrameLocation() + new Vector2(0, statusFrameSettings.Size.Y);
            }

            {
                this.PlusImmRectangle.Size = plusFrameSettings.Size;
                this.PlusFrameLocation = () => this.IdFrameLocation() + new Vector2(0, idFrameSettings.Size.Y);
            }

            {
                this.NameImmRectangle.Size = this.nameFrameSettings.Size;
                this.NameFrameLocation = () => this.StatusFrameLocation() + new Vector2(statusFrameSettings.Size.X, 0);
            }

            {
                this.DescriptionImmRectangle.Size = this.descriptionFrameSettings.Size;
                this.DescriptionFrameLocation = () => this.NameFrameLocation() + new Vector2(0, this.nameFrameSettings.Size.Y);
            }
        }

        #endregion

        #region Frames

        public MMPickableRectangleElement IdImmRectangle { get; private set; }

        public MMPickableRectangleElement PlusImmRectangle { get; set; }

        public MMPickableRectangleElement NameImmRectangle { get; private set; }

        public MMPickableRectangleElement DescriptionImmRectangle { get; private set; }

        public MMPickableRectangleElement StatusImmRectangle { get; private set; }

        public MMPickableRectangleElement StatisticsImmRectangle { get; set; }

        #endregion

        #region Positions

        public Func<Vector2> IdFrameLocation { get; protected set; }

        public Func<Vector2> PlusFrameLocation { get; set; }

        public Func<Vector2> StatusFrameLocation { get; protected set; }

        public Func<Vector2> StatisticsFrameLocation { get; set; }

        public Func<Vector2> NameFrameLocation { get; protected set; }

        public Func<Vector2> DescriptionFrameLocation { get; protected set; }

        #endregion

        #region Update

        public override void UpdateInput(GameTime time)
        {
            base.UpdateInput(time);

            this.IdImmRectangle.UpdateInput(time);
            this.PlusImmRectangle.UpdateInput(time);

            this.StatusImmRectangle.UpdateInput(time);
            this.StatisticsImmRectangle.UpdateInput(time);

            this.NameImmRectangle.UpdateInput(time);
            this.DescriptionImmRectangle.UpdateInput(time);
        }

        protected override void UpdateFrameGeometry()
        {
            this.RootImmRectangle.Location = this.RootFrameLocation().ToPoint();

            this.IdImmRectangle.Location = this.IdFrameLocation().ToPoint();
            this.PlusImmRectangle.Location = this.PlusFrameLocation().ToPoint();

            this.StatusImmRectangle.Location = this.StatusFrameLocation().ToPoint();
            this.StatisticsImmRectangle.Location =
                this.StatisticsFrameLocation().ToPoint();

            this.NameImmRectangle.Location = this.NameFrameLocation().ToPoint();
            this.DescriptionImmRectangle.Location =
                this.DescriptionFrameLocation().ToPoint();

            this.RootImmRectangle.Size = new Point(
                this.RootImmRectangle.Size.X,
                this.itemLayout.BlockRow * this.descriptionFrameSettings.Size.Y);

            this.DescriptionImmRectangle.Size = new Point(
                this.descriptionFrameSettings.Size.X,

                // this.itemLayout.BlockRow - 1 for taken position of name frame 
                (this.itemLayout.BlockRow - 1)
                * this.descriptionFrameSettings.Size.Y);
        }

        public override void Update(GameTime time)
        {
            this.RootImmRectangle.Update(time);

            this.IdImmRectangle.Update(time);
            this.PlusImmRectangle.Update(time);

            this.StatusImmRectangle.Update(time);
            this.StatisticsImmRectangle.Update(time);

            this.NameImmRectangle.Update(time);
            this.DescriptionImmRectangle.Update(time);

            this.UpdateFrameGeometry();
            this.UpdateFrameStates();
        }

        #endregion

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        this.IdImmRectangle?.Dispose();
                        this.PlusImmRectangle?.Dispose();
                        this.StatusImmRectangle?.Dispose();
                        this.StatisticsImmRectangle?.Dispose();
                        this.NameImmRectangle?.Dispose();
                        this.DescriptionImmRectangle?.Dispose();
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}