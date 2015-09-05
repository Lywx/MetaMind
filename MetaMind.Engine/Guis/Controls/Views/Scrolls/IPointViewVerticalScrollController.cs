namespace MetaMind.Engine.Guis.Controls.Views.Scrolls
{
    public interface IPointViewVerticalScrollController : IPointViewScrollController
    {
        #region State

        int RowOffset { get; set; }

        #endregion

        #region Display

        bool IsDownToDisplay(int id);

        bool IsUpToDisplay(int id);

        #endregion

        #region Operations

        void MoveDown();

        void MoveUp();

        void MoveUpToTop();

        #endregion
    }
}