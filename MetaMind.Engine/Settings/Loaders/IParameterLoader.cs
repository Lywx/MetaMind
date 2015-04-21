namespace MetaMind.Engine.Settings.Loaders
{
    public interface IParameterLoader<in T>
        where T : IParameter
    {
        void LoadParameter(T parameter);
    }
}