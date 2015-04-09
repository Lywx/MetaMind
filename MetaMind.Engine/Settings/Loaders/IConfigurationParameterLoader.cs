namespace MetaMind.Engine.Settings.Loaders
{
    public interface IConfigurationParameterLoader<in T>
        where T : IConfigurationParameter
    {
        void ParameterLoad(T parameter);
    }
}