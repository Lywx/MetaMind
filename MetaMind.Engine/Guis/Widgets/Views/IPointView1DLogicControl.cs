namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IPointView1DLogicControl : IViewControl
    {
        #region View Data

        bool AcceptInput { get; }

        bool Active { get; }

        #endregion

        #region Item Operations 

        void AddItem();

        #endregion

        #region Movement Operations 

        void FastMovePrevious();

        void FastMoveNext();

        void MovePrevious();

        void MoveNext();

        #endregion
    }
}