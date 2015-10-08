namespace MetaMind.Session.Guis.Widgets.IndexViews.Operations
{
    using System;
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Item.Frames;
    using Engine.Gui.Controls.Item.Layouts;
    using Engine.Gui.Controls.Item.Settings;
    using Engine.Gui.Elements.Rectangles;
    using Engine.Services;
    using Microsoft.Xna.Framework;

    public class OperationItemFrameController : BlcokViewVerticalItemFrameController
    {
        private ItemSettings itemSettings;

        private IBlockViewVerticalItemLayout itemLayout;

        private ViewItemVisualSettings nameFrameSettings;

        private ViewItemVisualSettings descriptionFrameSettings;

        public OperationItemFrameController(IViewItem item, ViewItemImmRectangle itemImmRootRectangle)
            : base(item, itemImmRootRectangle)
        {
            this.IdImmRectangle     = new MMPickableRectangleElement();
            this.StatusImmRectangle = new MMPickableRectangleElement();
            this.PlusImmRectangle   = new MMPickableRectangleElement();

            this.NameImmRectangle        = new MMPickableRectangleElement();
            this.DescriptionImmRectangle = new MMPickableRectangleElement();
        }

        ~OperationItemFrameController()
        {
            this.Dispose();
        }

        #region Layering

        public override void Initialize()
        {
            base.Initialize();
            
            var itemLayer = this.GetItemLayer<OperationItemLayer>();

            this.itemSettings = itemLayer.ItemSettings;
            this.itemLayout = itemLayer.ItemLogic.ItemLayout;

            var idFrameSettings = this.itemSettings.Get<ViewItemVisualSettings>("IdFrame");
            var statusFrameSettings = this.itemSettings.Get<ViewItemVisualSettings>("StatusFrame");
            var plusFrameSettings = this.itemSettings.Get<ViewItemVisualSettings>("PlusFrame");

            this.nameFrameSettings = this.itemSettings.Get<ViewItemVisualSettings>("NameFrame");
            this.descriptionFrameSettings = this.itemSettings.Get<ViewItemVisualSettings>("DescriptionFrame");

            // +------------+--------------+-----------------------+  
            // | id frame   |              | name frame            |
            // +------------+ status frame +-----------------------+
            // | plus frame |              | description frame     |
            // +------------+--------------+-----------------------+
            {
                this.IdImmRectangle.Size = idFrameSettings.Size;
                this.IdFrameLocation = this.RootFrameLocation;
            }

            {
                this.StatusImmRectangle.Size = statusFrameSettings.Size;
                this.StatusFrameLocation = () => this.IdFrameLocation() + new Vector2(idFrameSettings.Size.X, 0);
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

        #endregion

        #region Positions

        public Func<Vector2> IdFrameLocation { get; protected set; }

        public Func<Vector2> PlusFrameLocation { get; set; }

        public Func<Vector2> StatusFrameLocation { get; protected set; }

        public Func<Vector2> NameFrameLocation { get; protected set; }

        public Func<Vector2> DescriptionFrameLocation { get; protected set; }

        #endregion

        #region Update

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
        {
            base.UpdateInput(input, time);

            this.IdImmRectangle.UpdateInput(input, time);
            this.PlusImmRectangle.UpdateInput(input, time);

            this.StatusImmRectangle.UpdateInput(input, time);

            this.NameImmRectangle.UpdateInput(input, time);
            this.DescriptionImmRectangle.UpdateInput(input, time);
        }

        protected override void UpdateFrameGeometry()
        {
            this.RootImmRectangle.Location = this.RootFrameLocation().ToPoint();

            this.IdImmRectangle.Location = this.IdFrameLocation().ToPoint();
            this.PlusImmRectangle.Location = this.PlusFrameLocation().ToPoint();

            this.StatusImmRectangle.Location = this.StatusFrameLocation().ToPoint();

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
                        this.IdImmRectangle         ?.Dispose();
                        this.PlusImmRectangle       ?.Dispose();
                        this.StatusImmRectangle     ?.Dispose();
                        this.NameImmRectangle       ?.Dispose();
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