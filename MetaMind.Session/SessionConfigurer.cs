namespace MetaMind.Session
{
    using Console.Uniutils;
    using Engine;

    public class SessionConfigurer : IMMEngineConfigurer
    {
        public void Configure(MMEngine engine)
        {
            // Console
            var console = engine.Interop.Console;
            console.AddCommand(new ListCommand());
        }
    }
}