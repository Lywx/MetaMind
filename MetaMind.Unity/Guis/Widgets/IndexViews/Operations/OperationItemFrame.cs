namespace MetaMind.Unity.Guis.Widgets.IndexViews.Operations
{
    using System;
    using Engine.Guis.Elements;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Layouts;
    using Engine.Guis.Widgets.Items.Settings;
    using Engine.Services;
    using Microsoft.Xna.Framework;

    public class OperationItemFrame : BlcokViewVerticalItemFrame
    {
        private ItemSettings itemSettings;

        private IBlockViewVerticalItemLayout itemLayout;

        private FrameSettings nameFrameSettings;

        private FrameSettings descriptionFrameSettings;

        public OperationItemFrame(IViewItem item, IViewItemRootFrame itemRootFrame)
            : base(item, itemRootFrame)
        {
            this.IdFrame     = new PickableFrame();
            this.StatusFrame = new PickableFrame();
            this.PlusFrame   = new PickableFrame();

            this.NameFrame        = new PickableFrame();
            this.DescriptionFrame = new PickableFrame();
        }

        ~OperationItemFrame()
        {
            this.Dispose();
        }

        #region Layering

        public override void SetupLayer()
        {
            base.SetupLayer();
            
            var itemLayer = this.ItemGetLayer<OperationItemLayer>();

            this.itemSettings = itemLayer.ItemSettings;
            this.itemLayout = itemLayer.ItemLogic.ItemLayout;

            var idFrameSettings = this.itemSettings.Get<FrameSettings>("IdFrame");
            var statusFrameSettings = this.itemSettings.Get<FrameSettings>("StatusFrame");
            var plusFrameSettings = this.itemSettings.Get<FrameSettings>("PlusFrame");

            this.nameFrameSettings = this.itemSettings.Get<FrameSettings>("NameFrame");
            this.descriptionFrameSettings = this.itemSettings.Get<FrameSettings>("DescriptionFrame");

            // +------------+--------------+-----------------------+  
            // | id frame   |              | name frame            |
            // +------------+ status frame +-----------------------+
            // | plus frame |              | description frame     |
            // +------------+--------------+-----------------------+
            {
                this.IdFrame.Size = idFrameSettings.Size;
                this.IdFrameLocation = this.RootFrameLocation;
            }

            {
                this.StatusFrame.Size = statusFrameSettings.Size;
                this.StatusFrameLocation = () => this.IdFrameLocation() + new Vector2(idFrameSettings.Size.X, 0);
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

        #endregion

        #region Positions

        public Func<Vector2> IdFrameLocation { get; protected set; }

        public Func<Vector2> PlusFrameLocation { get; set; }

        public Func<Vector2> StatusFrameLocation { get; protected set; }

        public Func<Vector2> NameFrameLocation { get; protected set; }

        public Func<Vector2> DescriptionFrameLocation { get; protected set; }

        #endregion

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            base.UpdateInput(input, time);

            this.IdFrame.UpdateInput(input, time);
            this.PlusFrame.UpdateInput(input, time);

            this.StatusFrame.UpdateInput(input, time);

            this.NameFrame.UpdateInput(input, time);
            this.DescriptionFrame.UpdateInput(input, time);
        }

        protected override void UpdateFrameGeometry()
        {
            this.RootFrame.Location = this.RootFrameLocation().ToPoint();

            this.IdFrame.Location = this.IdFrameLocation().ToPoint();
            this.PlusFrame.Location = this.PlusFrameLocation().ToPoint();

            this.StatusFrame.Location = this.StatusFrameLocation().ToPoint();

            this.NameFrame.Location = this.NameFrameLocation().ToPoint();
            this.DescriptionFrame.Location =
                this.DescriptionFrameLocation().ToPoint();

            this.RootFrame.Size = new Point(
                this.RootFrame.Size.X,
                this.itemLayout.BlockRow * this.descriptionFrameSettings.Size.Y);

            this.DescriptionFrame.Size = new Point(
                this.descriptionFrameSettings.Size.X,

                // this.itemLayout.BlockRow - 1 for taken position of name frame 
                (this.itemLayout.BlockRow - 1)
                * this.descriptionFrameSettings.Size.Y);
        }

        public override void Update(GameTime time)
        {
            this.RootFrame.Update(time);

            this.IdFrame.Update(time);
            this.PlusFrame.Update(time);

            this.StatusFrame.Update(time);

            this.NameFrame.Update(time);
            this.DescriptionFrame.Update(time);

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
                        this.IdFrame         ?.Dispose();
                        this.PlusFrame       ?.Dispose();
                        this.StatusFrame     ?.Dispose();
                        this.NameFrame       ?.Dispose();
                        this.DescriptionFrame?.Dispose();
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