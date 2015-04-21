namespace MetaMind.Engine.Components
{
    using MetaMind.Engine.Components.Inputs;

    using Microsoft.Xna.Framework;

    public class GameEngineInput : IGameInput
    {
        public IInputEvent Event { get; private set; }

        public IInputState State { get; private set; }

        public GameEngineInput(GameEngine engine)
        {
            this.Event = new InputEvent(engine, 1);
            this.State = new InputState(engine, 2);
        }

        public void Initialize()
        {
        }

        public void UpdateInput(GameTime gameTime)
        {
            this.Event.UpdateInput(gameTime);
            this.State.UpdateInput(gameTime);   
        }
    }
}