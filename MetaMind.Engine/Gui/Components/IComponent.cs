namespace MetaMind.Engine.Gui.Components
{
    public interface IComponent
    {
        #region Initialization

        bool Initialized { get; }

        void Initialize();

        #endregion
    }
}