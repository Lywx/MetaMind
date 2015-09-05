namespace MetaMind.Engine.Guis.Controls.Views
{
    using System;
    using System.Linq;

    public abstract class ViewEntity : GameControllableEntity, IViewEntity
    {
        #region Constructors

        protected ViewEntity()
        {
            for (var i = 0; i < (int)ViewState.StateNum; i++)
            {
                this.states[i] = () => false;
            }

            this[ViewState.View_Is_Active] = () => true;
        }

        #endregion Constructors

        #region IViewEntity

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