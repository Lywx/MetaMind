namespace MetaMind.Engine.Core.Settings
{
    public interface IMMParameterDependant<in T>
        where T : IMMParameter
    {
        void LoadParameter(T parameter);
    }
}