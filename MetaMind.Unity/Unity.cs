// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Unity.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Unity
{
    using Components;
    using Engine;
    using Engine.Console.Commands.Coreutils;
    using Engine.Scripting.FSharp;
    using Engine.Session;
    using Microsoft.Xna.Framework;
    using Sessions;
    using Game = Engine.Game;

    public class Unity : Game
    {
        public Unity(GameEngine engine)
            : base(engine)
        {
            this.Engine.Components.Add(new ResourceMonitor(engine));

            this.Interop.Save = new SaveManager(engine);

            // Save CPU and GPU resource
            this.Interop.Screen.Settings.IsAlwaysActive  = false;
            this.Interop.Screen.Settings.IsAlwaysVisible = false;
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