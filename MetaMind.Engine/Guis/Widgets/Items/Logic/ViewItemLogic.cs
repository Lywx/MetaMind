namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Frames;
    using MetaMind.Engine.Guis.Widgets.Items.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Extensions;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewItemLogic : ViewItemComponent, IViewItemLogic
    {
        private readonly ViewSettings viewSettings;

        private bool isFrameInitialized;

        #region Constructors

        public ViewItemLogic(IViewItem item, IViewItemFrameControl itemFrame, IViewItemViewControl itemView, dynamic itemDataControl)
            : base(item)
        {
            this.viewSettings = this.ItemExtension.Get<ViewExtension>().ViewSettings;

            if (itemFrame == null)
            {
                throw new ArgumentNullException("itemFrame");
            }

            if (itemView == null)
            {
                throw new ArgumentNullException("itemView");
            }

            if (itemDataControl == null)
            {
                throw new ArgumentNullException("itemDataControl");
            }

            this.ItemFrame = itemFrame;
            this.ItemView  = itemView;
            this.ItemDataControl  = itemDataControl;

            this.PassInItemLogic();
        }

        private void PassInItemLogic()
        {
            this.Item[ItemState.Item_Is_Inputting] = this.ItemIsInputting;
        }

        protected virtual Func<bool> ItemIsInputting
        {
            get
            {
                return () => this.Item[ItemState.Item_Is_Active]() && 
                             this.Item[ItemState.Item_Is_Selected]() && 
                            !this.Item[ItemState.Item_Is_Editing]();
            }
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

        public IViewItemFrameControl ItemFrame { get; set; }

        public IViewItemViewControl ItemView { get; set; }

        #endregion

        public int Id { get; set; }

        #region IDisposable

        public override void Dispose()
        {
            if (this.ItemFrame != null)
            {
                this.ItemFrame.Dispose();
            }

            if (this.ItemDataControl != null)
            {
                ((IDisposable)this.ItemDataControl).Dispose();
            }

            base.Dispose();
        }

        #endregion 

        #region Update

        public override void Update(GameTime time)
        {
            // Reduce lagging graphics 
            if (!this.isFrameInitialized)
            {
                this.isFrameInitialized = true;
                this.ItemFrame.Update(time);
            }

            // For better performance
            if (this.Item[ItemState.Item_Is_Active]())
            {
                this.ItemFrame              .Update(time);
                ((IUpdateable)this.ItemDataControl).Update(time);
            }
        }

        public void UpdateView(GameTime gameTime)
        {
            // View activation is controlled by item view control
            this.ItemView.Update(gameTime);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (!this.Item[ItemState.Item_Is_Active]())
            {
                return;
            }

            // Mouse
            if (this.viewSettings.MouseEnabled)
            {
                this.ItemFrame.UpdateInput(input, time);
            }

            // Keyboard
            if (this.viewSettings.KeyboardEnabled)
            {
                ((IInputable)this.ItemDataControl).UpdateInput(input, time);
            }
        }
        #endregion Update
    }
}