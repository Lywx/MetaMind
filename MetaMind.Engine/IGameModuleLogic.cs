namespace MetaMind.Engine
{
    public interface IGameModuleLogic<out TModuleSettings> : IGameModuleComponent<TModuleSettings>, IOuterUpdateableOperations, IInputableOperations
    {
    }
}