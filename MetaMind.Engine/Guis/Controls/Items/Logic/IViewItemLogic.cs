namespace MetaMind.Engine.Guis.Controls.Items.Logic
{
    using Data;
    using Frames;
    using Interactions;
    using Layouts;
    using Microsoft.Xna.Framework;

    public interface IViewItemLogic : IViewItemComponent
    {
        #region Components

        IViewItemDataModel ItemModel { get; set; }

        IViewItemFrame ItemFrame { get; set; }

        IViewItemInteraction ItemInteraction { get; set; }

        IViewItemLayout ItemLayout { get; set; }

        #endregion

        void UpdateView(GameTime time);
    }
}