namespace MetaMind.Engine.Gui.Control
{
    public interface IComponent
    {
        #region Initialization

        bool Initialized { get; }

        void Initialize();

        #endregion
    }
}