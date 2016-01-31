namespace MetaMind.Session
{
    using Console.Uniutils;
    using Engine;
    using Engine.Core;

    public class MMSessionConfigurer : IMMEngineConfigurer
    {
        public void Configure(MMEngine engine)
        {
            // Console
            var console = engine.Interop.Console;
            console.AddCommand(new ListCommand());
        }
    }
}