namespace MetaMind.Engine.Screens
{
    using System;

    public class GameLayer : GameControllableEntity, IGameLayer
    {
        public GameLayer(IGameScreen screen, byte alpha = byte.MaxValue)
        {
            if (screen == null)
            {
                throw new ArgumentNullException("screen");
            }

            this.Alpha  = alpha;
            this.Screen = screen;

            this.IsActive = true;
        }

        public IGameScreen Screen { get; private set; }

        #region States

        public bool IsActive { get; set; }

        #endregion

        #region Graphics

        public byte Alpha { get; set; }

        #endregion
    }
}
