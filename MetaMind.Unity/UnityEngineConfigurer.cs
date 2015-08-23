namespace MetaMind.Unity
{
    using Engine;
    using Guis.Console.Commands;

    class UnityEngineConfigurer : GameEngineConfigurer
    {
        public override void Configure(GameEngine engine)
        {
            base.Configure(engine);

            engine.Interop.Console.AddCommand(new VerboseCommand());
        }
    }
}
