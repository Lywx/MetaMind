// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Unity.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Unity
{
    using Components;
    using Engine;
    using Engine.Service.Console.Commands.Coreutils;
    using Engine.Service.Scripting.FSharp;
    using Engine.Session;
    using Microsoft.Xna.Framework;
    using Sessions;

    public class Unity : MMGame
    {
        public Unity(MMEngine engine)
            : base(engine)
        {
            this.Engine.Components.Add(new ResourceMonitor(engine));

            this.Interop.Save = new MMSaveManager(engine);
            this.Game.Components.Add(this.Interop.Save);

            // Save CPU and GPU resource
            this.Interop.Screen.Settings.AlwaysActive  = false;
            this.Interop.Screen.Settings.AlwaysDraw = false;
        }

        #region Session Data

        public static ISession<SessionData, SessionController> Session { get; set; }

        #endregion

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            this.InitializeSession();
            this.InitializeConsole();
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