namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;
    using System.Linq;

    public abstract class ViewEntity : GameControllableEntity, IViewEntity
    {
        #region Constructors

        protected ViewEntity(ICloneable viewSettings, ICloneable itemSettings)
        {
            if (viewSettings == null)
            {
                throw new ArgumentNullException("viewSettings");
            }

            if (itemSettings == null)
            {
                throw new ArgumentNullException("itemSettings");
            }

            this.ViewSettings = viewSettings;
            this.ItemSettings = itemSettings;

            for (var i = 0; i < (int)ViewState.StateNum; i++)
            {
                this.states[i] = () => false;
            }

            this[ViewState.View_Is_Active] = () => true;
        }

        #endregion Constructors

        #region IViewEntity

        #region Settings

        public dynamic ItemSettings { get; private set; }

        public dynamic ViewSettings { get; private set; }

        #endregion

        #region States

        private readonly Func<bool>[] states = new Func<bool>[(int)ViewState.StateNum];

        public bool[] States
        {
            get
            {
                return this.states.Select(state => state()).ToArray();
            }
        }

        public Func<bool> this[ViewState state]
        {
            get
            {
                return this.states[(int)state];
            }

            set
            {
                this.states[(int)state] = value;
            }
        }

        #endregion

        #endregion IViewEntity
    }
}