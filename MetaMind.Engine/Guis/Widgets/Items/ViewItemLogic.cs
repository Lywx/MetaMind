namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewItemLogic : ViewItemComponent, IViewItemLogic
    {
        private bool isFrameInitialized;

        #region Constructors

        public ViewItemLogic(IViewItem item, dynamic itemFrameControl, dynamic itemViewControl, dynamic itemDataControl)
            : base(item)
        {
            if (itemFrameControl == null)
            {
                throw new ArgumentNullException("itemFrameControl");
            }

            if (itemViewControl == null)
            {
                throw new ArgumentNullException("itemViewControl");
            }

            if (itemDataControl == null)
            {
                throw new ArgumentNullException("itemDataControl");
            }

            this.ItemFrameControl = itemFrameControl;
            this.ItemViewControl  = itemViewControl;
            this.ItemDataControl  = itemDataControl;
        }

        #endregion

        #region Destructors

        ~ViewItemLogic()
        {
            this.Dispose();
        }

        #endregion

        #region Dependency

        public dynamic ItemDataControl { get; set; }

        public dynamic ItemFrameControl { get; set; }

        public dynamic ItemViewControl { get; set; }

        #endregion

        #region Indirect Dependency

        private new ViewSettings ViewSettings
        {
            get
            {
                return base.ViewSettings;
            }
        }

        #endregion

        public int Id { get; set; }

        #region IDisposable

        public override void Dispose()
        {
            if (this.ItemFrameControl != null)
            {
                ((IDisposable)this.ItemFrameControl).Dispose();
            }

            if (this.ItemDataControl != null)
            {
                ((IDisposable)this.ItemDataControl).Dispose();
            }

            base.Dispose();
        }

        #endregion 

        #region Update

        public virtual bool AcceptInput
        {
            get
            {
                return this.Item[ItemState.Item_Is_Active]() &&
                       this.Item[ItemState.Item_Is_Selected]() &&
                      !this.Item[ItemState.Item_Is_Editing]();
            }
        }

        public bool IsActive
        {
            get { return this.Item[ItemState.Item_Is_Active](); }
        }

        public override void Update(GameTime time)
        {
            this.UpdateForView(time);

            // Reduce lagging graphics 
            if (!this.isFrameInitialized)
            {
                this.isFrameInitialized = true;
                this.ItemFrameControl.Update(time);
            }

            // To improve performance
            if (this.IsActive)
            {
                this.ItemFrameControl.Update(time);
                this.ItemDataControl.Update(time);
            }
        }

        public void UpdateForView(GameTime gameTime)
        {
            // View activation is controlled by item view control
            this.ItemViewControl.Update(gameTime);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (!this.IsActive)
            {
                return;
            }

            // Mouse
            if (this.ViewSettings.MouseEnabled)
            {
                this.ItemFrameControl.UpdateInput(time);
            }

            // Keyboard
            if (this.ViewSettings.KeyboardEnabled)
            {
                this.ItemDataControl.UpdateInput(input, time);
            }
        }
        #endregion Update
    }
}