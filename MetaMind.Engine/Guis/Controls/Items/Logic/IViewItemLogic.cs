namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using MetaMind.Engine.Guis.Widgets.Items.Data;
    using MetaMind.Engine.Guis.Widgets.Items.Frames;
    using MetaMind.Engine.Guis.Widgets.Items.Interactions;
    using MetaMind.Engine.Guis.Widgets.Items.Layouts;

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