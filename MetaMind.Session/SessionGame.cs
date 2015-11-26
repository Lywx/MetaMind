namespace MetaMind.Session
{
    using Components;
    using Components.Input;
    using Engine;
    using Engine.Services.Console.Commands.Coreutils;
    using Engine.Services.Script.FSharp;
    using Engine.Sessions;
    using Microsoft.Xna.Framework;
    using Sessions;

    public class SessionGame : MMGame
    {
        public SessionGame(MMEngine engine)
            : base(engine)
        {
            this.Engine.Components.Add(new MMPerformanceMonitor(engine));

            this.Interop.Save = new SessionSaveManager(engine);
            this.Engine.Components.Add(this.Interop.Save);

            // Save CPU and GPU resource
            this.Interop.Screen.Settings.AlwaysActive = false;
            this.Interop.Screen.Settings.AlwaysDraw = false;
        }

        #region Session Data

        public static IMMSession<SessionData, SessionController> Session { get; set; }

        #endregion

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            this.InitializeActions();
            this.InitializeConsole();
            this.InitializeSession();
        }

        private void InitializeActions()
        {
            this.Input.State.Keyboard.LoadActions<SessionInputActions>();
        }

        private void InitializeConsole()
        {
            this.Interop.Console.AddCommand(new VerboseCommand(FsiSession));
        }

        private void InitializeSession()
        {
            // Load save to retrieve saved session
            this.Interop.Save.Load();

            Session.Controller.Initialize();

            // Update time related data before using Session.Cognition.Awake
            Session.Update();
        }

        #endregion Initialization

        #region Update

        public override void Update(GameTime time)
        {
            Session.Update();

            base.Update(time);
        }

        #endregion

        #region System

        public override void OnExiting()
        {
            this.Interop.Save.Save();

            this.Dispose();
        }

        #endregion System

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