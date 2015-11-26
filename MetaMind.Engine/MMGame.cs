namespace MetaMind.Engine
{
    using Components;

    public class MMGame : MMInputableComponent, IMMGame
    {
        #region Constructors

        protected MMGame(MMEngine engine)
            : base(engine)
        {
            this.Interop.Game.Add(this);
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
