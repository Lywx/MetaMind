namespace MetaMind.Engine.Screens
{
    public interface IGameLayer : IGameControllableEntity, IGameLayerOperations 
    {
        IGameScreen Screen { get; }

        #region State

        bool IsActive { get; set; }

        #endregion

        #region Graphics

        byte Alpha { get; set; }

        #endregion
    }
}