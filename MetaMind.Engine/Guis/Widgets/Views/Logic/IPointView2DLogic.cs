namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;

    public interface IPointView2DLogic : IViewLogic
    {
        new IPointView2DLayout ViewLayout { get; }

        new IPointView2DSelectionControl ViewSelection { get; }

        new IPointView2DScrollControl ViewScroll { get; }

        #region Item Operations

        void AddItem();

        #endregion Item Operations

        #region Movement Operations

        void FastMoveDown();

        void FastMoveLeft();

        void FastMoveRight();

        void FastMoveUp();

        void MoveDown();

        void MoveLeft();

        void MoveRight();

        void MoveUp();

        #endregion Movement Operations
    }
}