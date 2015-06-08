namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    public interface IViewLogicOperations
    {
        #region Binding

        void SetupBinding();

        #endregion

        #region Control

        void AddItem();

        void AddItem(dynamic data);

        #endregion
    }
}