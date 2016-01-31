namespace MetaMind.Engine.Core.Entity.Control.Views.Controllers
{
    using System;
    using Layouts;
    using Scrolls;
    using Selections;

    public interface IMMPointViewVerticalController : IMMViewController 
    {
        new IMMPointViewVerticalLayout ViewLayout { get; }

        new IMMPointViewVerticalSelectionController ViewSelection { get; }

        new IMMPointViewVerticalScrollController ViewScroll { get; }

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