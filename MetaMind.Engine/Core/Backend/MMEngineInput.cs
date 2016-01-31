namespace MetaMind.Engine.Core.Backend
{
    using Windows.Components.Input;
    using Input;
    using Microsoft.Xna.Framework;

#if WINDOWS

#endif

    public class MMEngineInput : MMGeneralComponent, IMMEngineInput
    {
        public MMEngineInput(MMEngine engine) 
            : base(engine)
        {
            // Event and State won't be added to the Game.Components collection, 
            // for it is better to control them manually

#if WINDOWS
            this.Event = new MMInputEventWin32(engine)
#endif
            {
                UpdateOrder = 1
            };

            this.State = new MMInputState(engine)
            {
                UpdateOrder = 2
            };
        }

        public IMMInputEvent Event { get; private set; }

        public IMMInputState State { get; private set; }

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