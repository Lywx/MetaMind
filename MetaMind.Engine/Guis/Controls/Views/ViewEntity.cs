namespace MetaMind.Engine.Guis.Controls.Views
{
    using System;
    using System.Linq;

    public abstract class ViewEntity : Control, IViewEntity
    {
        #region Constructors

        protected ViewEntity()
        {
            this.InitializeStates();
        }

        #endregion Constructors

        #region Initialization
        
        private void InitializeStates()
        {
            for (var i = 0; i < (int)ViewState.StateNum; ++i)
            {
                this.viewStates[i] = () => false;
            }

            this[ViewState.View_Is_Active] = () => true;
        }

        #endregion 

        #region States

        private readonly Func<bool>[] viewStates = new Func<bool>[(int)ViewState.StateNum];

        /// <summary>
        /// View states.
        /// </summary>
        internal bool[] ViewStates
        {
            get
            {
                return this.viewStates.Select(state => state()).ToArray();
            }
        }

        public Func<bool> this[ViewState state]
        {
            get
            {
                return this.viewStates[(int)state];
            }

            set
            {
                this.viewStates[(int)state] = value;
            }
        }

        #endregion
    }
}