namespace MetaMind.Acutance.Components
{
    using System;

    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Services;
    using MetaMind.Engine.Sessions;

    public class SaveManager : Engine.Components.SaveManager
    {
        #region Dependency

        private IGameInteropService Interop { get; set; }

        #endregion

        #region Constructors

        public SaveManager(GameEngine engine)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }
        }

        public override void Initialize()
        {
            // Dependency
            var engine = (GameEngine)this.Game;
            this.Interop = engine.Interop;

            base.Initialize();
        }

        #endregion

        public override void Save()
        {
            Acutance.Session.Save();

            // Trigger an event to inform other components to refresh states
            this.Interop.Event.TriggerEvent(new Event((int)SessionEventType.SessionSaved, null));
        }

        public override void Load()
        {
            Acutance.Session = Session<SessionData>.Load();
        }
    }
}