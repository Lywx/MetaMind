namespace MetaMind.Engine.Core
{
    using Backend;

    public class MMGame : MMGeneralComponent, IMMGame
    {
        #region Constructors

        protected MMGame(MMEngine engine)
            : base(engine)
        {
            this.GlobalInterop.Game.Add(this);
        }

        #endregion

        #region IMMGame

        public virtual void OnExiting() {}

        public void Run()
        {
            this.Engine.Run();
        }

        #endregion
    }
}
