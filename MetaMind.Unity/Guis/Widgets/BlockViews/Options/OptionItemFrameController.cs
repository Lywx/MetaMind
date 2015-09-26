namespace MetaMind.Unity.Guis.Widgets.BlockViews.Options
{
    using System;
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Item.Frames;
    using Engine.Gui.Controls.Item.Layouts;
    using Engine.Gui.Controls.Item.Settings;
    using Engine.Gui.Elements.Rectangles;
    using Engine.Service;
    using Microsoft.Xna.Framework;

    public class OptionItemFrameController : BlcokViewVerticalItemFrameController
    {
        private ItemSettings itemSettings;

        private IBlockViewVerticalItemLayout itemLayout;

        private FrameSettings descriptionFrameSettings;

        public OptionItemFrameController(IViewItem item, ViewItemRectangle itemRootRectangle)
            : base(item, itemRootRectangle)
        {
            this.IdRectangle = new PickableRectangle();
            this.NameRectangle = new PickableRectangle();

            this.DescriptionRectangle = new PickableRectangle();
        }

        ~OptionItemFrameController()
        {
            this.Dispose();
        }

        #region Layering

        public override void Initialize()
        {
            base.Initialize();
            
            var itemLayer = this.GetItemLayer<OptionItemLayer>();

            this.itemSettings = itemLayer.ItemSettings;
            this.itemLayout = itemLayer.ItemLogic.ItemLayout;

            var idFrameSettings = this.itemSettings.Get<FrameSettings>("IdFrame");
            var nameFrameSettings = this.itemSettings.Get<FrameSettings>("NameFrame");
            this.descriptionFrameSettings = this.itemSettings.Get<FrameSettings>("DescriptionFrame");

            // +----------+--------------------------------------+  
            // | id frame | name frame                           |
            // +----------+--------------------------------------+  
            // | description frame                               |
            // +-------------------------------------------------+  
            {
                this.IdRectangle.Size = idFrameSettings.Size;
                this.IdFrameLocation = this.RootFrameLocation;
            }

            {
                this.NameRectangle.Size = nameFrameSettings.Size;
                this.NameFrameLocation = () => this.IdFrameLocation() + new Vector2(idFrameSettings.Size.X, 0);
            }

            {
                this.DescriptionRectangle.Size = this.descriptionFrameSettings.Size;
                this.DescriptionFrameLocation = () => this.IdFrameLocation() + new Vector2(0, idFrameSettings.Size.Y);
            }
        }

        #endregion

        #region Frames

        public PickableRectangle IdRectangle { get; private set; }

        public PickableRectangle NameRectangle { get; private set; }

        public PickableRectangle DescriptionRectangle { get; private set; }

        #endregion

        #region Positions

        public Func<Vector2> IdFrameLocation { get; protected set; }

        public Func<Vector2> NameFrameLocation { get; protected set; }

        public Func<Vector2> DescriptionFrameLocation { get; protected set; }

        #endregion

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            base.UpdateInput(input, time);

            this.IdRectangle.UpdateInput(input, time);
            this.NameRectangle.UpdateInput(input, time);
            this.DescriptionRectangle.UpdateInput(input, time);
        }

        protected override void UpdateFrameGeometry()
        {
            this.RootRectangle.Location = this.RootFrameLocation().ToPoint();

            this.IdRectangle.Location = this.IdFrameLocation().ToPoint();
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