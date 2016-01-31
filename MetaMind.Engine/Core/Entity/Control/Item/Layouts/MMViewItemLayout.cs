namespace MetaMind.Engine.Core.Entity.Control.Item.Layouts
{
    using System;
    using Interactions;
    using Microsoft.Xna.Framework;
    using Views;

    public class MMViewItemLayout : MMViewItemControllerComponent, IMMViewItemLayout
    {
        private readonly IViewItemLayoutInteraction itemLayoutInteraction;


        #region Constructors

        protected MMViewItemLayout(IMMViewItem item, IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item)
        {
            if (itemLayoutInteraction == null)
            {
                throw new ArgumentNullException(nameof(itemLayoutInteraction));
            }

            this.itemLayoutInteraction = itemLayoutInteraction;
        }

        #endregion

        #region Layout Data

        public virtual int Id { get; set; }

        #endregion

        #region Logic Data

        private Func<bool> itemIsActive;

        public Func<bool> ItemIsActive
        {
            get { return this.itemIsActive; }
            set
            {
                this.itemIsActive = value;
                this.Item[MMViewItemState.Item_Is_Active] = value;
            }
        }

        private void InitializeLogic()
        {
            if (this.ItemIsActive == null)
            {
                this.ItemIsActive =
                    () => this.View[MMViewState.View_Is_Active]() &&
                          this.itemLayoutInteraction.ViewCanDisplay(this);
            }
        }

        #endregion

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            this.InitializeLogic();
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.UpdateId();
        }

        protected void UpdateId()
        {
            this.Id = this.View.Items.IndexOf(this.Item);
        }

        #endregion
    }
}