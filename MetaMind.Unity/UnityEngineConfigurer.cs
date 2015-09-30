namespace MetaMind.Unity
{
    using Console.Uniutils;
    using Engine;

    public class UnityEngineConfigurer : IMMEngineConfigurer
    {
        public void Configure(MMEngine engine)
        {
            // Console
            var console = engine.Interop.Console;
            console.AddCommand(new ListCommand());
        }
    }
}