namespace MetaMind.Engine.Entities
{
    /// <summary>
    /// IReactor is just a interface for object that needs initialization before 
    /// use.
    /// </summary>
    public interface IMMReactor
    {
        #region Initialization

        bool Initialized { get; }

        void Initialize();

        #endregion
    }
}