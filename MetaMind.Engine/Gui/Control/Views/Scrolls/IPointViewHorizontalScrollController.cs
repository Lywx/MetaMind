namespace MetaMind.Engine.Gui.Control.Views.Scrolls
{
    public interface IPointViewHorizontalScrollController : IPointViewScrollController
    {
        #region State

        int ColumnOffset { get; }

        #endregion

        #region Display

        bool IsLeftToDisplay(int id);

        bool IsRightToDisplay(int id);

        #endregion

        #region Operations

        void MoveLeft();

        void MoveRight();

        #endregion
    }
}