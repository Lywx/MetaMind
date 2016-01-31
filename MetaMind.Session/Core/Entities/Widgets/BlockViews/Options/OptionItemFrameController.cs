namespace MetaMind.Session.Guis.Widgets.BlockViews.Options
{
    using System;
    using Engine.Core.Entity.Control.Item;
    using Engine.Core.Entity.Control.Item.Frames;
    using Engine.Core.Entity.Control.Item.Layouts;
    using Engine.Core.Entity.Control.Item.Settings;
    using Engine.Core.Entity.Input;
    using Microsoft.Xna.Framework;

    public class OptionItemFrameController : MMBlockViewVerticalItemFrameController
    {
        private ItemSettings itemSettings;

        private IMMBlockViewVerticalItemLayout itemLayout;

        private MMViewItemRenderSettings descriptionFrameSettings;

        public OptionItemFrameController(IMMViewItem item, ViewItemImmRectangle itemImmRootRectangle)
            : base(item, itemImmRootRectangle)
        {
            this.IdImmRectangle = new MMPickableRectangleElement();
            this.NameImmRectangle = new MMPickableRectangleElement();

            this.DescriptionImmRectangle = new MMPickableRectangleElement();
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

            var idFrameSettings = this.itemSettings.Get<MMViewItemRenderSettings>("IdFrame");
            var nameFrameSettings = this.itemSettings.Get<MMViewItemRenderSettings>("NameFrame");
            this.descriptionFrameSettings = this.itemSettings.Get<MMViewItemRenderSettings>("DescriptionFrame");

            // +----------+--------------------------------------+  
            // | id frame | name frame                           |
            // +----------+--------------------------------------+  
            // | description frame                               |
            // +-------------------------------------------------+  
            {
                this.IdImmRectangle.Size = idFrameSettings.Size;
                this.IdFrameLocation = this.RootFrameLocation;
            }

            {
                this.NameImmRectangle.Size = nameFrameSettings.Size;
                this.NameFrameLocation = () => this.IdFrameLocation() + new Vector2(idFrameSettings.Size.X, 0);
            }

            {
                this.DescriptionImmRectangle.Size = this.descriptionFrameSettings.Size;
                this.DescriptionFrameLocation = () => this.IdFrameLocation() + new Vector2(0, idFrameSettings.Size.Y);
            }
        }

        #endregion

        #region Frames

        public MMPickableRectangleElement IdImmRectangle { get; private set; }

        public MMPickableRectangleElement NameImmRectangle { get; private set; }

        public MMPickableRectangleElement DescriptionImmRectangle { get; private set; }

        #endregion

        #region Positions

        public Func<Vector2> IdFrameLocation { get; protected set; }

        public Func<Vector2> NameFrameLocation { get; protected set; }

        public Func<Vector2> DescriptionFrameLocation { get; protected set; }

        #endregion

        #region Update

        public override void UpdateInput(GameTime time)
        {
            base.UpdateInput(time);

            this.IdImmRectangle.UpdateInput(time);
            this.NameImmRectangle.UpdateInput(time);
            this.DescriptionImmRectangle.UpdateInput(time);
        }

        protected override void UpdateFrameGeometry()
        {
            this.RootImmRectangle.Location = this.RootFrameLocation().ToPoint();

            this.IdImmRectangle.Location = this.IdFrameLocation().ToPoint();
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