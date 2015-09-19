namespace MetaMind.Unity.Guis.Widgets.IndexViews.Operations
{
    using System;
    using Engine.Gui.Control.Item;
    using Engine.Gui.Control.Item.Frames;
    using Engine.Gui.Control.Item.Layouts;
    using Engine.Gui.Control.Item.Settings;
    using Engine.Gui.Element.Rectangles;
    using Engine.Service;
    using Microsoft.Xna.Framework;

    public class OperationItemFrameController : BlcokViewVerticalItemFrameController
    {
        private ItemSettings itemSettings;

        private IBlockViewVerticalItemLayout itemLayout;

        private FrameSettings nameFrameSettings;

        private FrameSettings descriptionFrameSettings;

        public OperationItemFrameController(IViewItem item, ViewItemRectangle itemRootRectangle)
            : base(item, itemRootRectangle)
        {
            this.IdRectangle     = new PickableRectangle();
            this.StatusRectangle = new PickableRectangle();
            this.PlusRectangle   = new PickableRectangle();

            this.NameRectangle        = new PickableRectangle();
            this.DescriptionRectangle = new PickableRectangle();
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
                this.IdRectangle.Size = idFrameSettings.Size;
                this.IdFrameLocation = this.RootFrameLocation;
            }

            {
                this.StatusRectangle.Size = statusFrameSettings.Size;
                this.StatusFrameLocation = () => this.IdFrameLocation() + new Vector2(idFrameSettings.Size.X, 0);
            }

            {
                this.PlusRectangle.Size = plusFrameSettings.Size;
                this.PlusFrameLocation = () => this.IdFrameLocation() + new Vector2(0, idFrameSettings.Size.Y);
            }

            {
                this.NameRectangle.Size = this.nameFrameSettings.Size;
                this.NameFrameLocation = () => this.StatusFrameLocation() + new Vector2(statusFrameSettings.Size.X, 0);
            }

            {
                this.DescriptionRectangle.Size = this.descriptionFrameSettings.Size;
                this.DescriptionFrameLocation = () => this.NameFrameLocation() + new Vector2(0, this.nameFrameSettings.Size.Y);
            }
        }

        #endregion

        #region Frames

        public PickableRectangle IdRectangle { get; private set; }

        public PickableRectangle PlusRectangle { get; set; }

        public PickableRectangle NameRectangle { get; private set; }

        public PickableRectangle DescriptionRectangle { get; private set; }

        public PickableRectangle StatusRectangle { get; private set; }

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

            this.IdRectangle.UpdateInput(input, time);
            this.PlusRectangle.UpdateInput(input, time);

            this.StatusRectangle.UpdateInput(input, time);

            this.NameRectangle.UpdateInput(input, time);
            this.DescriptionRectangle.UpdateInput(input, time);
        }

        protected override void UpdateFrameGeometry()
        {
            this.RootRectangle.Location = this.RootFrameLocation().ToPoint();

            this.IdRectangle.Location = this.IdFrameLocation().ToPoint();
            this.PlusRectangle.Location = this.PlusFrameLocation().ToPoint();

            this.StatusRectangle.Location = this.StatusFrameLocation().ToPoint();

            this.NameRectangle.Location = this.NameFrameLocation().ToPoint();
            this.DescriptionRectangle.Location =
                this.DescriptionFrameLocation().ToPoint();

            this.RootRectangle.Size = new Point(
                this.RootRectangle.Size.X,
                this.itemLayout.BlockRow * this.descriptionFrameSettings.Size.Y);

            this.DescriptionRectangle.Size = new Point(
                this.descriptionFrameSettings.Size.X,

                // this.itemLayout.BlockRow - 1 for taken position of name frame 
                (this.itemLayout.BlockRow - 1)
                * this.descriptionFrameSettings.Size.Y);
        }

        public override void Update(GameTime time)
        {
            this.RootRectangle.Update(time);

            this.IdRectangle.Update(time);
            this.PlusRectangle.Update(time);

            this.StatusRectangle.Update(time);

            this.NameRectangle.Update(time);
            this.DescriptionRectangle.Update(time);

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
                        this.IdRectangle         ?.Dispose();
                        this.PlusRectangle       ?.Dispose();
                        this.StatusRectangle     ?.Dispose();
                        this.NameRectangle       ?.Dispose();
                        this.DescriptionRectangle?.Dispose();
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