namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IPointListControl : IPointViewControl
    {
        PointViewRegion Region { get; }

        bool AcceptInput { get; }

        bool Active { get; }

        void AddItem();

        void MoveLeft();

        void MoveRight();

        void FastMoveLeft();

        void FastMoveRight();
    }
}