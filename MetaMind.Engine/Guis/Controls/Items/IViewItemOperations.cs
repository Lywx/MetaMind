namespace MetaMind.Engine.Guis.Controls.Items
{
    using Layers;
    using Microsoft.Xna.Framework;

    public interface IViewItemOperations
    {
        #region Layer

        T GetLayer<T>() where T : class, IViewItemLayer;

        void SetupLayer();

        #endregion

        #region Update

        /// <summary>
        /// Update logic related with IView.
        /// </summary>
        /// <param name="time"></param>
        void UpdateView(GameTime time);

        #endregion
    }
}