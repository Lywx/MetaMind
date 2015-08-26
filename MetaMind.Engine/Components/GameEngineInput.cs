namespace MetaMind.Engine.Components
{
    using Inputs;

    using Microsoft.Xna.Framework;

    public class GameEngineInput : GameControllableComponent, IGameInput
    {
        public IInputEvent Event { get; private set; }

        public IInputState State { get; private set; }

        public GameEngineInput(GameEngine engine) 
            : base(engine)
        {
            this.Event = new InputEvent(engine)
            {
                UpdateOrder = 1
            };

            this.State = new InputState(engine)
            {
                UpdateOrder = 2
            };
        }

        public override void Initialize()
        {
        }

        public override void UpdateInput(GameTime gameTime)
        {
            this.Event.UpdateInput(gameTime);
            this.State.UpdateInput(gameTime);   
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Event.Dispose();
                this.Event = null;

                this.State.Dispose();
                this.State = null;
            }

            base.Dispose(disposing);
        }
    }
}