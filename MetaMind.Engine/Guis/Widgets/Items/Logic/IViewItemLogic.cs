namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using MetaMind.Engine.Guis.Widgets.Items.Frames;
    using MetaMind.Engine.Guis.Widgets.Items.Views;

    using Microsoft.Xna.Framework;

    public interface IViewItemLogic : IViewItemComponent, IUpdateable, IInputable
    {
        #region Components

        dynamic ItemDataControl { get; set; }

        IViewItemFrameControl ItemFrame { get; set; }

        IViewItemViewControl ItemView { get; set; }

        #endregion

        #region Item Identification

        int Id { get; set; }

        #endregion

        void UpdateView(GameTime time);
    }
}