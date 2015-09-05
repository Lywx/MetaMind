namespace MetaMind.Engine.Guis.Controls.Views.Scrolls
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