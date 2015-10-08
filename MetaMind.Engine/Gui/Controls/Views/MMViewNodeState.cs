namespace MetaMind.Engine.Gui.Controls.Views
{
    using System;
    using System.Linq;
    using Nodes;

    public abstract class MMViewNodeState : MMNode
    {
        #region Constructors

        protected MMViewNodeState()
        {
            this.InitializeStates();
        }

        #endregion Constructors

        #region Initialization
        
        private void InitializeStates()
        {
            for (var i = 0; i < (int)MMViewState.StateNum; ++i)
            {
                this.viewStates[i] = () => false;
            }

            this[MMViewState.View_Is_Active] = () => true;
        }

        #endregion 

        #region States

        private readonly Func<bool>[] viewStates = new Func<bool>[(int)MMViewState.StateNum];

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

        public Func<bool> this[MMViewState state]
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