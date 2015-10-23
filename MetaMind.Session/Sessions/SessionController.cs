namespace MetaMind.Session.Sessions
{
    using System;
    using System.Speech.Synthesis;
    using Engine;
    using Engine.Components.Interop.Event;
    using Engine.Entities.Controls;
    using Engine.Services;
    using Engine.Services.Script.FSharp;
    using Engine.Services.Script.IronPython;
    using Engine.Sessions;
    using Guis.Screens;

    public class SessionController : MMObject, IMMSessionController<SessionData>
    {
        private BackgroundScreenSettings background;

        #region Constructors and Finalizer

        public SessionController()
        {
        }

        ~SessionController()
        {
            this.Dispose(true);
        }

        #endregion

        #region Session Data

        /// <summary>
        /// It is injected from setter.
        /// </summary>
        public SessionData Data { get; set; }

        #endregion

        #region Components

        public FsiSession FsiSession { get; set; }

        public IpySession IpySession { get; set; }

        public SpeechSynthesizer Speech { get; set; }

        #endregion Components

        #region Initialization

        public void Initialize()
        {
            this.InitializeScriptEngine();
            this.InitializeSynthesizer();

            this.LoadContent();

            // Has to be last, adding screen involves loading all kind of things
            this.InitializeScreen();
        }


        private void InitializeSynthesizer()
        {
            this.Speech = new SpeechSynthesizer
            {
                Volume = 100,
                Rate = 3
            };
        }

        private void InitializeScriptEngine()
        {
            // Start F# session 
            this.FsiSession = new FsiSession();

            // Start IronPython session 
            this.IpySession = new IpySession();
        }

        private void InitializeScreen()
        {
            this.background = new BackgroundScreenSettings();

            this.Interop.Screen.AddScreen(new StartScreen());

            this.Interop.Screen.AddScreen(new BackgroundScreen(this.background));
            this.Interop.Event.QueueEvent(new MMEvent((int)SessionEvent.GameStarted, null, 3, 1));

            var consciousness = this.Data.Cognition.Consciousness;
            if (consciousness.IsAwake)
            {
                this.Interop.Screen.AddScreen(new MainScreen());
            }
            else
            {
                this.Interop.Screen.AddScreen(new SummaryScreen());
            }
        }

        #endregion

        #region Load and Unload

        public void LoadContent()
        {
            this.Interop.Asset.LoadPackage("Session.Persistent");
        }

        public void UnloadContent()
        {
        }

        #endregion

        #region Update

        public void Update()
        {
            this.UpdateScriptEngine();
        }

        private void UpdateScriptEngine()
        {
            this.FsiSession.Update();
            this.IpySession.Update();
        }

        #endregion

        #region IDisposable

        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!this.IsDisposed)
                {
                    this.DisposeScriptEngine();
                    this.DisposeSynthesizer();
                }

                this.IsDisposed = true;
            }
        }

        private void DisposeSynthesizer()
        {
            this.Speech?.Dispose();
        }

        private void DisposeScriptEngine()
        {
            this.FsiSession?.Dispose();
            this.IpySession?.Dispose();
        }

        #endregion
    }
}
