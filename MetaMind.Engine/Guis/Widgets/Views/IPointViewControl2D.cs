namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IPointViewControl2D
    {
        bool AcceptInput { get; }

        bool Active { get; }

        int ColumnNum { get; }

        int RowNum { get; }

        void AddItem();

        int ColumnFrom(int id);

        int IdFrom(int i, int j);

        void MoveDown();

        void MoveLeft();

        void MoveRight();

        void MoveUp();

        int RowFrom(int id);

        void SuperMoveDown();

        void SuperMoveLeft();

        void SuperMoveRight();

        void SuperMoveUp();
    }
}