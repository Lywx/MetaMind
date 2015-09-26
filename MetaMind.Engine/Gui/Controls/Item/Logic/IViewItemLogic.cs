namespace MetaMind.Engine.Gui.Controls.Item.Logic
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

        IViewItemFrameController ItemFrame { get; set; }

        IViewItemInteraction ItemInteraction { get; set; }

        IViewItemLayout ItemLayout { get; set; }

        #endregion

        void UpdateView(GameTime time);
    }
}