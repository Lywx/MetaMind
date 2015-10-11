namespace MetaMind.Engine.Entities.Controls.Views.Scrolls
{
    public interface IMMPointViewHorizontalScrollController : IMMPointViewScrollController
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