namespace MetaMind.Engine.Gui.Controls.Views.Logic
{
    using System;
    using Layouts;
    using Scrolls;
    using Selections;

    public interface IMMPointViewVerticalController : IMMViewController 
    {
        new IPointViewVerticalLayout ViewLayout { get; }

        new IPointViewVerticalSelectionController ViewSelection { get; }

        new IPointViewVerticalScrollController ViewScroll { get; }

        #region Events

        event EventHandler ScrolledUp;

        event EventHandler ScrolledDown;

        event EventHandler MovedUp;

        event EventHandler MovedDown;

        #endregion

        #region Operations

        void ScrollDown();

        void ScrollUp();

        void MoveDown();

        void FastMoveDown();

        void MoveUp();

        void FastMoveUp();

        #endregion
    }
}