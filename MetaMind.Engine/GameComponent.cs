namespace MetaMind.Engine
{
    using Services;

    public class GameComponent : Microsoft.Xna.Framework.GameComponent
    {
        protected GameComponent(GameEngine engine) : base(engine)
        {
            this.SetupService();
        }

        protected GameEngine Engine { get; private set; }

        protected IGameInteropService GameInterop { get; set; }

        protected IGameGraphicsService GameGraphics { get; set; }

        protected IGameNumericalService GameNumerical { get; set; }

        private void SetupService()
        {
            this.Engine = ((GameEngine)this.Game);

            this.GameInterop   = GameEngine.Service?.Interop;
            this.GameGraphics  = GameEngine.Service?.Graphics;
            this.GameNumerical = GameEngine.Service?.Numerical;
        }
    }
}
