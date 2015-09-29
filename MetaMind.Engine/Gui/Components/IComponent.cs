namespace MetaMind.Engine.Gui.Components
{
    public interface IComponent
    {
        #region States

        bool Active { get; set; }

        #endregion

        #region Initialization

        bool Initialized { get; }

        void Initialize();

        void CheckInitialization();

        #endregion
    }
}