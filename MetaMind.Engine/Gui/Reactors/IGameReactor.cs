namespace MetaMind.Engine.Gui.Reactors
{
    public interface IGameReactor
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