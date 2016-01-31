namespace MetaMind.Engine.Core.Entity.Control.Views.Controllers
{
    public interface IMMViewControllerOperations
    {
        #region Binding

        void LoadBinding();

        void UnloadBinding();

        #endregion

        #region Control

        void AddItem();

        void AddItem(dynamic data);

        void ResetItems();

        #endregion
    }
}