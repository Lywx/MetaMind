namespace MetaMind.Engine.Core.Entity.Control.Item
{
    using Data;
    using Frames;
    using Interactions;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Views;

    public interface IMMViewItemControllerBase : IMMViewItemControllerComponent
    {
        
    }

    public interface IMMViewItemController : IMMViewItemControllerBase
    {
        IMMView View { get; }

        IMMViewItem Item { get; }

        #region Components

        IMMViewItemDataModel ItemModel { get; set; }

        IMMViewItemFrameController ItemFrame { get; set; }

        IMMViewItemInteraction ItemInteraction { get; set; }

        IMMViewItemLayout ItemLayout { get; set; }

        #endregion

        void UpdateView(GameTime time);
    }
}