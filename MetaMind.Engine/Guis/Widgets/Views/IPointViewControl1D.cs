namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IPointViewControl1D : IViewControl
    {
        #region View Data

        bool AcceptInput { get; }

        bool Active { get; }

        #endregion

        #region Item Operations 

        void AddItem();

        #endregion

        #region Movement Operations 

        void FastMoveLeft();

        void FastMoveRight();

        void MoveLeft();

        void MoveRight();

        #endregion
    }
}