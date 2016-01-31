namespace MetaMind.Engine.Core.Entity.Control.Views.Scrolls
{
    public interface IMMPointViewVerticalScrollController : IMMPointViewScrollController
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