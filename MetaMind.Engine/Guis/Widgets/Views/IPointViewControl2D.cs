namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IPointViewControl2D : IViewControl
    {
        #region View Data

        bool AcceptInput { get; }

        bool Active { get; }

        int ColumnNum { get; }

        int RowNum { get; }

        int ColumnFrom(int id);

        int IdFrom(int i, int j);

        int RowFrom(int id);

        #endregion View Data

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