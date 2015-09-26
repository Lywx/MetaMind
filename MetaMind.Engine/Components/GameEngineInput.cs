namespace MetaMind.Engine.Components
{
    using Input;
    using Microsoft.Xna.Framework;

    public class GameEngineInput : GameControllableComponent, IGameInput
    {
        public GameEngineInput(GameEngine engine) 
            : base(engine)
        {
            // Event and State won't be added to the Game.Components collection, 
            // for it is better to control them manually

            this.Event = new InputEvent(engine)
            {
                UpdateOrder = 1
            };

            this.State = new InputState(engine)
            {
                UpdateOrder = 2
            };
        }

        public IInputEvent Event { get; private set; }

        public IInputState State { get; private set; }

        public override void Initialize()
        {
            this.Event.Initialize();
            this.State.Initialize();
        }

        public override void UpdateInput(GameTime time)
        {
            this.Event.UpdateInput(time);
            this.State.UpdateInput(time);   
        }

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        this.Event?.Dispose();
                        this.Event = null;

                        this.State?.Dispose();
                        this.State = null;
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}