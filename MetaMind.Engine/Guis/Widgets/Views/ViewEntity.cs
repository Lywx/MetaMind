namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    public interface IViewEntity : IInputable, IDrawable, IUpdateable
    {
        dynamic ItemSettings { get; }

        bool[] States { get; }

        dynamic ViewSettings { get; }

        void Disable(ViewState state);

        void Enable(ViewState state);

        bool IsEnabled(ViewState state);
    }

    public abstract class ViewEntity : InputableGameEntity, IViewEntity
    {
        #region Constructors

        protected ViewEntity(ICloneable viewSettings, ICloneable itemSettings)
        {
            this.ViewSettings = viewSettings;
            this.ItemSettings = itemSettings;

            this.Enable(ViewState.View_Active);
        }

        #endregion Constructors

        #region IViewEntity

        private bool[] states = new bool[(int)ViewState.StateNum];

        public dynamic ItemSettings { get; private set; }

        public bool[] States { get { return this.states; } }

        public dynamic ViewSettings { get; private set; }

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

        #endregion IViewEntity
    }
}