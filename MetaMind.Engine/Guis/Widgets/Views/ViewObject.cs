namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using Microsoft.Xna.Framework;

    public interface IViewObject : IManualInputable
    {
        bool[] States { get; }

        dynamic ViewSettings { get; }

        dynamic ItemSettings { get; }

        void Disable(ViewState state);

        void Enable(ViewState state);

        bool IsEnabled(ViewState state);
    }

    public class ViewObject : ManualInputGameElement, IViewObject
    {
        public dynamic ViewSettings { get; private set; }

        public dynamic ItemSettings { get; private set; }

        protected ViewObject(ICloneable viewSettings, ICloneable itemSettings)
        {
            this.ViewSettings = viewSettings;
            this.ItemSettings = itemSettings;

            this.Enable(ViewState.View_Active);
        }

        private bool[] states = new bool[(int)ViewState.StateNum];

        public bool[] States { get { return this.states; } }

        public override void Draw(GameTime gameTime, byte alpha)
        {
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
        }

        public override void UpdateStructure(GameTime gameTime)
        {
        }

        #region Helper Methods

        public void Disable(ViewState state)
        {
            state.DisableStateIn(this.states);
        }

        public void Enable(ViewState state)
        {
            state.EnableStateIn(this.states);
        }

        public bool IsEnabled(ViewState state)
        {
            return state.IsStateEnabledIn(this.states);
        }

        #endregion Helper Methods
    }
}