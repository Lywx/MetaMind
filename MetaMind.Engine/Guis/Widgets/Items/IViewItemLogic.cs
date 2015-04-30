namespace MetaMind.Engine.Guis.Widgets.Items
{
    public interface IViewItemLogic
    {
        #region Components

        dynamic ItemDataControl { get; set; }

        dynamic ItemFrameControl { get; set; }

        dynamic ItemViewControl { get; set; }

        #endregion

        #region States

        bool AcceptInput { get; }

        bool IsActive { get; }

        #endregion

        #region Item Identification

        int Id { get; set; }

        #endregion
    }
}